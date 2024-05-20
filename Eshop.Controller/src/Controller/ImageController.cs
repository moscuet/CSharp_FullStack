using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Core.src.ValueObject;
using Eshop.Core.src.Common;

namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IReviewService _reviewService;

        public ImageController(IImageService imageService, IReviewService reviewService)
        {
            _imageService = imageService;
            _reviewService = reviewService;
        }

        // POST: Create a new image
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ImageReadDTO>> CreateAsync([FromBody] ImageCreateDTO imageDto)
        {
        var (userId, userRole) = UserContextHelper.GetUserClaims(HttpContext);

            if (imageDto.EntityType == EntityType.Product)
            {
                if (userRole != "Admin")
                    return Forbid();
            }
            else if (imageDto.EntityType == EntityType.Review)
            {
                // Only the owner of the review can create images for the review
                var review = await _reviewService.GetByIdAsync(imageDto.EntityId);
                if (review.UserId != userId)
                    return Forbid();
            }

            var createdImage = await _imageService.CreateAsync(imageDto);
            return Ok(createdImage);
        }

        // GET: Image by id

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageReadDTO>> GetByIdAsync(Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
        }

        // GET: Get all images for an entity
        [HttpGet("entity/{entityId}")]
        public async Task<ActionResult<IEnumerable<ImageReadDTO>>> GetImagesByEntityIdAsync(Guid entityId)
        {
            var images = await _imageService.GetImagesByEntityIdAsync(entityId);
            return Ok(images);
        }


        // DELETE: Delete an image by ID
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            var (userId, userRole) = UserContextHelper.GetUserClaims(HttpContext);

            if (userRole != "Admin" && !await IsOwnerOfReviewImage(image, userId.Value))
                return Forbid();

            await _imageService.DeleteByIdAsync(id);
            return NoContent();
        }

        // GET: Get all images
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageReadDTO>>> GetAllImagesAsync([FromQuery] QueryOptions options)
        {
            var images = await _imageService.GetAllImagesAsync(options);
            return Ok(images);
        }

        private async Task<bool> IsOwnerOfReviewImage(ImageReadDTO image, Guid userId)
        {
            if (image.EntityType == EntityType.Review)
            {
                var review = await _reviewService.GetByIdAsync(image.EntityId);
                return review != null && review.UserId == userId;
            }
            return false;
        }
    }

    
    
}

