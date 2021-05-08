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
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public ReviewController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetApprovedReviews()
        {
            var reviews = _repository.Review.GetReviewsByStatus("Approved", trackChanges: false);
            if(reviews == null)
            {
                _logger.LogInfo($"No reviews found.");
                return NotFound();
            }

            var reviewsDto = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }
    }
}
