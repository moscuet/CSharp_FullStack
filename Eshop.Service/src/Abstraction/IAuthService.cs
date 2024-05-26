using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(UserCredential credential);
    }
}
