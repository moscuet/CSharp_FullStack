using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controller.src.Controller
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<TokenDTO> LoginAsync(UserCredential userCredential)
        {
            return await _authService.LoginAsync(userCredential);
        }
    }
}