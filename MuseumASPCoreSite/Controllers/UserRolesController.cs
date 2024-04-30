using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumASPCoreSite.Contracts.Requests;
using MuseumSite.Domain.Entitites;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]

    public class UserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<UserEntity> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole([FromForm] string RoleName)
        {
            if(string.IsNullOrEmpty(RoleName))
            {
                return BadRequest("RoleName should not to be empty");
            }

            if (await _roleManager.RoleExistsAsync(RoleName))
            {
                return BadRequest("Role already exist");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(RoleName));

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("DeleteRole")]
        public async Task<ActionResult> DeleteRole([FromForm]string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetUserRole")]
        public async Task<ActionResult> GetUserRole([FromForm]string email)
        {
            var user = await _userManager.FindByEmailAsync(email);  
            if(user == null)
            {
                return BadRequest("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }


        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult> AddRoleToUser([FromForm]UserRoleRequest userRole)
        {
            var user = await _userManager.FindByEmailAsync(userRole.email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, userRole.roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("RemoveRoleFromUser")]
        public async Task<ActionResult> RemoveRoleFromUser([FromForm]UserRoleRequest userRole)
        {
            var user = await _userManager.FindByEmailAsync(userRole.email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, userRole.roleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
