using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IUserService : IBaseService<UserCreateDTO, UserUpdateDTO, UserReadDTO>
    {
        Task<IEnumerable<UserReadDTO>> GetAllUsersAsync(QueryOptions options);
    }
}
