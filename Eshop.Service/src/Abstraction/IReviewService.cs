using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IReviewService : IBaseService<ReviewCreateDTO, ReviewUpdateDTO, ReviewReadDTO>
    {
        Task<IEnumerable<ReviewReadDTO>> GetAllReviewsAsync(QueryOptions options);
        Task<bool> DeleteReviewByIdAsync(Guid userId, Guid id);
    }
}
