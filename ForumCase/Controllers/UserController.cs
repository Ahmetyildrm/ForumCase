using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumCase.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public UserController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _repository.User.GetAllUsers(trackChanges: false);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUser(Guid id)
        {
            var user = _repository.User.GetUser(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
        }

        [HttpGet("{userEmail}/{userPassword}/reviews", Name = "GetUserReviews")]
        public IActionResult GetReviewsForUser(string userEmail, string userPassword)
        {
            var user = _repository.User.GetUserWithEmail(userEmail, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with email: {userEmail} doesn't exist in the database.");
                return NotFound();
            }
            if (!userPassword.Equals(user.Password))
            {
                _logger.LogInfo($"Password doesn't match with user Email {userEmail}");
                return NotFound();
            }
            
            var reviewsFromDb = _repository.Review.GetReviews(user.Id,
                trackChanges: false);
            if(!reviewsFromDb.Any())
            {
                _logger.LogInfo($"User with Email: {userEmail} has no Reviews");
                return NotFound();
            }

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviewsFromDb);

            return Ok(reviewsDto);
        }

        [HttpGet("{userEmail}/{userPassword}/reviews/{id}", Name = "GetReviewForUser")]
        public IActionResult GetReviewForUser(string userEmail, Guid id, string userPassword)
        {
            var user = _repository.User.GetUserWithEmail(userEmail, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with Email: {userEmail} doesn't exist in the database.");
                return NotFound();
            }

            if (!userPassword.Equals(user.Password))
            {
                _logger.LogInfo($"Password doesn't match with user Email {userEmail}");
                return NotFound();
            }

            var reviewDb = _repository.Review.GetReview(user.Id, id, trackChanges: false);

            if (reviewDb == null)
            {
                _logger.LogInfo($"Review with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(reviewDb);

            return Ok(review);
        }

        [HttpPost("register", Name = "RegisterUser")]
        public IActionResult CreateUser([FromBody] UserForCreationDto user)
        {
            if (user == null)
            {
                _logger.LogError("UserForCreationDto object sent from client is null.");
                return BadRequest("UserForCreationDto object is null");
            }

            var users = _repository.User.GetAllUsers(trackChanges: false);
            foreach(var dbUser in users)
            {
                if (dbUser.Email.Equals(user.Email))
                {
                    _logger.LogError($"This email {user.Email} is already used.");
                    return BadRequest("This email is used");
                }
            }

            if(user.Password.Length < 8)
            {
                _logger.LogError($"Password must be longer or equal than 8 characters");
                return BadRequest("Short password");
            }

            var userEntity = _mapper.Map<User>(user);

            _repository.User.CreateUser(userEntity);
            _repository.Save();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("UserById", new { id = userToReturn.Id },
                userToReturn);
        }

        [HttpPost("{userEmail}/{userPassword}/makereview")]
        public IActionResult CreateReviewForUser(string userEmail, string userPassword, [FromBody]
        ReviewForCreationDto review)
        {
            if (review == null)
            {
                _logger.LogError("ReviewCreationDto object sent from client is null.");
                return BadRequest("ReviewForCreationDto object is null");
            }

            var user = _repository.User.GetUserWithEmail(userEmail, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with Email {userEmail} doesn't exist in the database.");
                return NotFound();
            }

            if (!userPassword.Equals(user.Password))
            {
                _logger.LogInfo($"Password doesn't match with user Email {userEmail}");
                return NotFound();
            }

            var reviewEntity = _mapper.Map<Review>(review);
            reviewEntity.Star = 0;
            reviewEntity.OparatedBy = "Not Operated Yet";
            reviewEntity.Status = "Pending";
            reviewEntity.UserId = user.Id;

            _repository.Review.CreateReview(user.Id, reviewEntity);
            _repository.Save();

            var reviewToReturn = _mapper.Map<ReviewDto>(reviewEntity);

             return CreatedAtRoute("GetReviewForUser", new {userEmail, id =
                reviewEntity.Id, userPassword}, reviewToReturn);
        }

        [HttpDelete("{userEmail}/{userPassword}/reviews/delete/{id}")]
        public IActionResult DeleteReviewForUser(string userEmail, string userPassword, Guid id)
        {
            var user = _repository.User.GetUserWithEmail(userEmail, trackChanges: false);
            if (user == null) 
            { 
                _logger.LogInfo($"Company with Email: {userEmail} doesn't exist in the database.");
                return NotFound(); 
            }
            if (!userPassword.Equals(user.Password))
            {
                _logger.LogInfo($"Password doesn't match with user Email {userEmail}");
                return NotFound();
            }
            var review = _repository.Review.GetReview(user.Id, id, trackChanges: false); 
            if (review == null) 
            { 
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database."); 
                return NotFound(); 
            }

            if (!review.UserId.Equals(user.Id))
            {
                _logger.LogInfo($"This is not your review");
                return NotFound();
            }
            _repository.Review.DeleteReview(review);
            _repository.Save(); 
            return NoContent();
        }

        [HttpPut("{userEmail}/{userPassword}/reviews/update/{id}")]
        public IActionResult UpdateReviewForUser(string userEmail, string userPassword, Guid id,
            [FromBody] ReviewForUpdateDto review)
        {
            if (review == null)
            {
                _logger.LogError("EmploteeForUpdateDto object sent from client is null.");
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            var user = _repository.User.GetUserWithEmail(userEmail, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"Company with Email: {userEmail} doesn't exist in the database.");
                return NotFound();
            }
            if (!userPassword.Equals(user.Password))
            {
                _logger.LogInfo($"Password doesn't match with user Email {userEmail}");
                return NotFound();
            }

            var reviewEntity = _repository.Review.GetReview(user.Id, id, trackChanges: true);
            if (reviewEntity == null)
            {
                _logger.LogInfo($"Employee with {id} doesn't exist in the database.");
                return NotFound();
            }
            if (!reviewEntity.UserId.Equals(user.Id))
            {
                _logger.LogInfo($"This is not your review");
                return NotFound();
            }

            _mapper.Map(review, reviewEntity);
            _repository.Save();

            return NoContent();
        }
    }
}