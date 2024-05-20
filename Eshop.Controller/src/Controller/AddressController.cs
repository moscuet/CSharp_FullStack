using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;
using System.Security.Claims;
using System.Text.Json;

namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // POST: Create a new address
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AddressReadDTO>> CreateAsync([FromBody] AddressCreateDTO addressDto)
        { 
             var (currentUserId,_) = UserContextHelper.GetUserClaims(HttpContext);


            addressDto.UserId = currentUserId;

            var createdAddress = await _addressService.CreateAsync(addressDto);
            return Ok(createdAddress);
        }

        // GET: Get address by ID ( only owner or admin )
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressReadDTO>> GetByIdAsync(Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
                return NotFound();

        var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);

            if (address.UserId != currentUserId && currentUserRole != "Admin")
            {
                return Forbid();
            }

            return Ok(address);
        }

        // PUT: Update an address (only owner)
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddressUpdateDTO addressDto)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
                return NotFound();

        var (currentUserId, _) = UserContextHelper.GetUserClaims(HttpContext);

            if (address.UserId != currentUserId)
                return Forbid();

            await _addressService.UpdateAsync(id, addressDto);
            return NoContent();
        }


        // DELETE: Delete an address by ID ( Only owner)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null)
                return NotFound();
 
        var (currentUserId,_) = UserContextHelper.GetUserClaims(HttpContext);

            if (address.UserId != currentUserId)
                return Forbid();

            await _addressService.DeleteByIdAsync(id);
            return NoContent();
        }

        // GET: Get all addresses for a user ( only owner or admin )
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AddressReadDTO>>> GetAllUserAddressesAsync(Guid userId)
        {
        var (currentUserId, currentUserRole) = UserContextHelper.GetUserClaims(HttpContext);

            if (currentUserId != userId && currentUserRole != "Admin")
                return Forbid();

            var addresses = await _addressService.GetAllUserAddressesAsync(userId);
            return Ok(addresses);
        }
    }
}
