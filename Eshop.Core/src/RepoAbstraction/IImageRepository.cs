using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<IEnumerable<Image>> GetImagesByEntityIdAsync(Guid entityId);
        Task<IEnumerable<Image>> GetAllImagesAsync(QueryOptions options);
    }
}