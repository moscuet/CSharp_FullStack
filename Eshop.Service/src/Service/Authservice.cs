using Eshop.Core.src.Common;
using Eshop.Core.src.RepoAbstraction;
using Eshop.Core.src.ValueObject;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.Service.src.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;

        public AuthService(IUserRepository userRepo, IUserService userService, ITokenService tokenService, IPasswordService passwordService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        public async Task<string> LoginAsync(UserCredential userCredential)
        {
            var user = await _userRepo.GetUserByEmailAsync(userCredential.Email);
            if (user == null || !_passwordService.VerifyPassword(userCredential.Password, user.Password, user.Salt))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }
            return _tokenService.GenerateToken(user, TokenType.AccessToken);
        }

        public Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}