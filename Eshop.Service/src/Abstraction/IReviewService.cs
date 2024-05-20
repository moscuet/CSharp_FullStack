using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IReviewService : IBaseService<ReviewCreateDTO, ReviewUpdateDTO, ReviewReadDTO>
    {
        Task<IEnumerable<ReviewReadDTO>> GetReviewsByProductIdAsync(Guid productId);
        Task<IEnumerable<ReviewReadDTO>> GetReviewsByUserIdAsync(Guid userId);
        Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(QueryOptions options);

    }
}
