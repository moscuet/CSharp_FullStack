using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;

namespace Eshop.Core.src.RepoAbstraction
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserByIdAsync(User user);
        Task<User> GetUserByCredentialAsync(UserCredential credential);
        Task<IEnumerable<User>> GetAllUsersAsync(QueryOptions options);
        Task<User>? GetUserByIdAsync(Guid id);
        Task<bool> DeleteUserByIdAsync(Guid id);
        Task<bool> UserExistsByEmailAsync(string email);
    }
}
