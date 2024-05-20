using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

using AutoMapper;
using System.Text.Json;
using Eshop.Core.src.ValueObject;

// using System.Transactions;

// Console.WriteLine("@@@@@@@@@@@@@@###########################");
// Console.WriteLine(JsonSerializer.Serialize(reviewCreateDto));

namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IImageService imageService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // POST: Create a new review
       [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReviewReadDTO>> CreateAsync([FromBody] ReviewCreateControllerDTO reviewDto)
        {
            var (userId, _) = UserContextHelper.GetUserClaims(HttpContext);

            var reviewCreateDto = _mapper.Map<ReviewCreateDTO>(reviewDto);
            reviewCreateDto.UserId = userId.Value;

            var createdReview = await _reviewService.CreateAsync(reviewCreateDto);

            return Ok(createdReview);
        }



        // GET: Review by id
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDTO>> GetByIdAsync(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // PUT: Update a review
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReviewUpdateDTO reviewDto)
        {
            var (userId, _) = UserContextHelper.GetUserClaims(HttpContext);

            var review = await _reviewService.GetByIdAsync(id);

            if (review == null)
                return NotFound("Review not found.");

            if (review.UserId != userId.Value)
                return Forbid();

            await _reviewService.UpdateAsync(id, reviewDto);

            return NoContent();
        }

        // DELETE: Delete a review
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var (userId, _) = UserContextHelper.GetUserClaims(HttpContext);
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound("Review not found.");

            await _reviewService.DeleteByIdAsync(id);

            return NoContent();
        }

        // GET: Get all reviews
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDTO>>> GetAllAsync([FromQuery] QueryOptions options)
        {
            var reviews = await _reviewService.GetAllReviewsAsync(options);
            return Ok(reviews);
        }

        // GET: Reviews by product ID
        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewReadDTO>>> GetReviewsByProductIdAsync(Guid productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }

        // GET: Reviews by user ID
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReviewReadDTO>>> GetReviewsByUserIdAsync(Guid userId)
        {
            var (currentUserId, userRole) = UserContextHelper.GetUserClaims(HttpContext);

            if (!currentUserId.HasValue || (currentUserId.Value != userId && userRole != "Admin"))
                return Forbid("Access denied");

            var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
            if (reviews == null)
                return NotFound("No reviews found for the given user.");

            return Ok(reviews);
        }
    }
}
