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

     public  async Task<Review>  ReviewCreateAsync(ReviewCreateDTO reviewCreateDto)
        {
            var review = _mapper.Map<Review>(reviewCreateDto);
            var createdReview = await _reviewRepository.CreateWithImagesAsync(review, reviewCreateDto.ImageUrls);
            return createdReview;
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


