using Eshop.Core.src.Entity;
namespace Eshop.Core.src.RepositoryAbstraction
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<IEnumerable<Image>> GetImagesByEntityIdAsync(Guid entityId);
    }
}