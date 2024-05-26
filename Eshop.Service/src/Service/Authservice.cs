using AutoMapper;
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
        private readonly IMapper _mapper;


        public AuthService(IUserRepository userRepo, IUserService userService, ITokenService tokenService, IPasswordService passwordService, IMapper mapper)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<TokenDTO> LoginAsync(UserCredential userCredential)
        {
            var userExist = await _userRepo.GetUserByEmailAsync(userCredential.Email);
            if (userExist == null || !_passwordService.VerifyPassword(userCredential.Password, userExist.Password, userExist.Salt))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }
            var token = _tokenService.GenerateToken(userExist, TokenType.AccessToken);
            var userReadDto = _mapper.Map<UserReadDTO>(userExist);
            return new TokenDTO
            {
                AccessToken = token,
                User = userReadDto
            };
        }
    }
}