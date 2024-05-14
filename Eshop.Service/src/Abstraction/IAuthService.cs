using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.ServiceAbstraction
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserCredential credential);
        Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
        Task<bool> RevokeTokenAsync(string refreshToken);
    }
}
