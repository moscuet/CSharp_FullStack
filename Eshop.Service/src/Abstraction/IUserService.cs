using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IUserService
    {
        Task<UserReadDTO> CreateUserAsync(UserCreateDTO user);
        Task<bool> UpdateUserByIdAsync(Guid id,UserUpdateDTO user);
        Task<UserReadDTO> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserReadDTO>> GetAllUsersAsync(QueryOptions options);
        Task<bool> DeleteUserByIdAsync(Guid id);
    }
}
