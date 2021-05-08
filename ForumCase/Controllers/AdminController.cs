using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumCase.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public AdminController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{username}/{password}/reviews")]
        public IActionResult GetAllReviews(string username, string password)
        {
            var admin = _repository.Admin.GetAdmin(username, trackChanges: false);
            if (admin == null)
            {
                _logger.LogInfo($"User with username: {username} doesn't exist in the database.");
                return NotFound();
            }
            if (!admin.Password.Equals(password))
            {
                _logger.LogInfo($"Password doesn't match with Admin {username}");
                return NotFound();
            }
            var reviews = _repository.Review.GetAllReviews(trackChanges: false);
            if (reviews == null)
            {
                _logger.LogInfo($"No reviews found.");
                return NotFound();
            }

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }

        [HttpGet("{username}/{password}/reviews/{id}")]
        public IActionResult GetReview(string username, string password, Guid id)
        {
            var admin = _repository.Admin.GetAdmin(username, trackChanges: false);
            if (admin == null)
            {
                _logger.LogInfo($"User with username: {username} doesn't exist in the database.");
                return NotFound();
            }
            if (!admin.Password.Equals(password))
            {
                _logger.LogInfo($"Password doesn't match with Admin {username}");
                return NotFound();
            }
            var reviewDb = _repository.Review.GetReviewById(id, trackChanges: false);

            if (reviewDb == null)
            {
                _logger.LogInfo($"Review with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(reviewDb);

            return Ok(review);
        }

        [HttpPut("{username}/{password}/reviews/{id}/status/{status}")]
        public IActionResult CheckReview(string username, string password, Guid id,
            string status)
        {   
            var admin = _repository.Admin.GetAdmin(username, trackChanges: false);
            if (admin == null)
            {
                _logger.LogInfo($"User with username: {username} doesn't exist in the database.");
                return NotFound();
            }
            if (!admin.Password.Equals(password))
            {
                _logger.LogInfo($"Password doesn't match with Admin {username}");
                return NotFound();
            }

            var reviewDb = _repository.Review.GetReviewById(id, trackChanges: true);

            if (reviewDb == null)
            {
                _logger.LogInfo($"Review with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if(reviewDb.Status == "Approved")
            {
                _logger.LogInfo($"Review with id: {id} is already approved");
                return BadRequest("Review is already approved");
            }

            if(!status.Equals("Approved") && !status.Equals("Rejected"))
            {
                _logger.LogInfo($"You can only change status to Rejected or Approved");
                return BadRequest("You can only change status to Rejected or Approved");
            }
            reviewDb.Status = status;
            reviewDb.OparatedBy = admin.Username;
            
            _repository.Save();

            var reviewdt = _mapper.Map<ReviewDto>(reviewDb);

            return Ok(reviewdt);
        }

        [HttpPut("{username}/{password}/reviews/{id}/star/{star}")]
        public IActionResult UpdateReviewForUser(string username, string password, Guid id,
            string star)
        {
            var admin = _repository.Admin.GetAdmin(username, trackChanges: false);
            if (admin == null)
            {
                _logger.LogInfo($"User with username: {username} doesn't exist in the database.");
                return NotFound();
            }
            if (!admin.Password.Equals(password))
            {
                _logger.LogInfo($"Password doesn't match with Admin {username}");
                return NotFound();
            }

            var reviewDb = _repository.Review.GetReviewById(id, trackChanges: true);

            if (reviewDb == null)
            {
                _logger.LogInfo($"Review with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if (reviewDb.Status != "Approved")
            {
                _logger.LogInfo($"Review must be Approved first");
                return BadRequest("Review must be Approved first");
            }

            if(!int.TryParse(star, out _))
            {
                _logger.LogInfo($"Star must be numeric");
                return BadRequest("Star must be numeric");
            }

            if (int.Parse(star) < 0 || int.Parse(star) > 5)
            {
                _logger.LogInfo($"Star must be between 0-5");
                return BadRequest("Star must be between 0-5");
            }

            reviewDb.Star = int.Parse(star);

            _repository.Save();

            var reviewdt = _mapper.Map<ReviewDto>(reviewDb);

            return Ok(reviewdt);
        }
    }
}
