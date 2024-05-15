using Microsoft.AspNetCore.Mvc;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Core.src.Common;
using System.Text.Json;

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

        // POST: api/v1/users
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> CreateUserAsync([FromBody] UserCreateDTO userDTO)
        {
            Console.WriteLine($"Received role: {userDTO.UserRole}");  // Check received role
            var createdUser = await _userService.CreateUserAsync(userDTO);
            return Ok(createdUser);
        }

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        // GET: api/v1/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsersAsync([FromQuery] QueryOptions options)
        {
            var users = await _userService.GetAllUsersAsync(options);
            return Ok(users);
        }

        // PUT: api/v1/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdateDTO userDTO)
        {
            bool updateResult = await _userService.UpdateUserByIdAsync(id, userDTO);
            if (!updateResult)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        // DELETE: api/v1/users/{id}
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
