using System.Text.Json;
using AutoMapper;
using Eshop.Core.src.Common;
using Eshop.Core.src.RepositoryAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class ReviewService : BaseService<Review, ReviewCreateDTO, ReviewUpdateDTO, ReviewReadDTO>, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
            : base(reviewRepository, mapper)
        {
            _reviewRepository = reviewRepository;
        }


     public override async Task<ReviewReadDTO> CreateAsync(ReviewCreateDTO reviewCreateDto)
        {
            // Convert ReviewCreateDTO to Review entity
            var review = _mapper.Map<Review>(reviewCreateDto);

            // Log the DTO before creating the review
            Console.WriteLine($"004: {JsonSerializer.Serialize(reviewCreateDto)}");
            Console.WriteLine($"005: {JsonSerializer.Serialize(review)}");

            // Create the review with images
            var createdReview = await _reviewRepository.CreateWithImagesAsync(review, reviewCreateDto.ImageUrls);
            return _mapper.Map<ReviewReadDTO>(createdReview);
        }
    

        public async Task<IEnumerable<ReviewReadDTO>> GetReviewsByProductIdAsync(Guid productId)
        {
            var reviews = await _reviewRepository.GetReviewsByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ReviewReadDTO>>(reviews);
        }

        public async Task<IEnumerable<ReviewReadDTO>> GetReviewsByUserIdAsync(Guid userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ReviewReadDTO>>(reviews);
        }


        public async Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(QueryOptions options)
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync(options);
            return _mapper.Map<IEnumerable<ReviewReadDTO>>(reviews);
        }
    }
}
