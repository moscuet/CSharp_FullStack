using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;

public interface IProductLineRepository : IBaseRepository<ProductLine>
{
    Task<IEnumerable<ProductLine>> GetAllProductLinesAsync(QueryOptions options);
}
