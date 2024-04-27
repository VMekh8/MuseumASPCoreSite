using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
    }
}
