using Eshop.Core.src.Common;
using static System.Net.Mime.MediaTypeNames;

namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<Review> CreateWithImagesAsync(Review review, List<string> imageUrls);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
        Task<IEnumerable<Review>> GetAllReviewsAsync(QueryOptions options);
    }
}