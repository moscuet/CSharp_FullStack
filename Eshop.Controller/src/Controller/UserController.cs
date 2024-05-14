using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eshop.Core.src.Common;
using Eshop.Service.src.ServiceAbstraction;
using System.Security.Claims;
using Eshop.Service.src.DTO;

namespace WebDemo.Controller.src.Controller
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles ="Admin")]// authentication middleware would be invoked if user send get request to this endpoint
        [HttpGet("")] // define endpoint: /users?page=1&pageSize=10
        public async Task<IEnumerable<UserReadDTO>> GetAllUsersAsync([FromQuery] QueryOptions options)
        {
            try
            {
                return await _userService.GetAllUsersAsync(options);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        // only admin can get user profile by id
        //[Authorize(Roles ="Admin")]
        [HttpGet("{id}")] // define endpoint: /users/{id}
        public async Task<UserReadDTO> GetUserByIdAsync([FromRoute] Guid id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        // user needs to be logged in to check her own profile
       // [Authorize]
        [HttpGet("profile")]
        public async Task<UserReadDTO> GetUserProfileAsync()
        {
            var claims = HttpContext.User; // not user obbject, but user claims
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _userService.GetUserByIdAsync(userId);
        }
    }
}