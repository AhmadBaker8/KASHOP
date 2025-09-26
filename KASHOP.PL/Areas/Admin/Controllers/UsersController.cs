using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsync(userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("isblocked/{userId}")]
        public async Task<IActionResult> IsBlockedUser([FromRoute] string userId)
        {
            var result = await _userService.IsBlockedAsync(userId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPatch("changeRole/{userId}")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> ChangeUserRole([FromRoute] string userId, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, request.RoleName);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { Message = "User role updated successfully" });
        }


    }
}