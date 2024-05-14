
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
namespace Eshop.Service.src.ServiceAbstraction
{
    public interface ITokenService
    {
        string GenerateToken(User user,TokenType type);
    }
}