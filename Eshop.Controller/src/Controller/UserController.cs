using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;
using Eshop.Core.src.Common;
using Eshop.Core.src.ValueObject;

namespace Eshop.Controller.src.Controller
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

        // Register user
        [HttpPost("register")]
        public async Task<ActionResult<UserReadDTO>> CreateUserAsync([FromBody] UserCreateDTO userDTO)
        {
            userDTO.UserRole = UserRole.User;
            var createdUser = await _userService.CreateUserAsync(userDTO);
            return Ok(createdUser);
        }

        // GET: User profile
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserReadDTO>> GetUserProfileAsync()
        {
            var claims = HttpContext.User;
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.GetUserProfileAsync(userId);
            return Ok(user);
        }

        // PUT: Update Profile
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserUpdateDTO userDTO)
        {
            var claims = HttpContext.User;
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool updateResult = await _userService.UpdateUserByIdAsync(userId, userDTO);
            if (!updateResult)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        // DELETE: Delete Profile
        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteUserProfileAsync()
        {
            var claims = HttpContext.User;
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool deleteResult = await _userService.DeleteUserByIdAsync(userId);
            if (!deleteResult)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        // GET: api/v1/users/{id}
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserProfileAsync(id);
            return Ok(user);
        }

        // GET: api/v1/users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsersAsync([FromQuery] QueryOptions options)
        {
            var users = await _userService.GetAllUsersAsync(options);
            return Ok(users);
        }


        // Create admin (Super Admin only)
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-admin")]
        public async Task<ActionResult<UserReadDTO>> CreateAdminAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            userCreateDTO.UserRole = UserRole.Admin;
            var createdUser = await _userService.CreateUserAsync(userCreateDTO);
            return Ok(createdUser);
        }

        // DELETE: api/v1/users/{id} (Super Admin only)
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            bool deleteResult = await _userService.DeleteUserByIdAsync(id);
            if (!deleteResult)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }
    }
}
