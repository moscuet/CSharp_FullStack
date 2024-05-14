using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(ReviewCreateDTO review);
        Task<bool> UpdateReviewByIdAsync(Guid userId,Guid reviewId,ReviewUpdateDTO review);
        Task<ReviewReadDTO> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(QueryOptions options);
        Task<bool> DeleteReviewByIdAsync(Guid userId,Guid id);
    }
}
