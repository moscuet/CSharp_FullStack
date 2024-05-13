using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Core.src.RepositoryAbstraction;
namespace Eshop.Core.src.RepoAbstraction
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByCredentialAsync(UserCredential credential);
        Task<IEnumerable<User>> GetAllUsersAsync(QueryOptions options);
        Task<bool> UserExistsByEmailAsync(string email);
    }
}