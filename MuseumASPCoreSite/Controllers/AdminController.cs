using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;

        public AdminController(UserManager<UserEntity> userManager, IUserService userService, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserResponce>>> GetAllUser()
        {
            var user = await _userService.GetUsersAsync();

            if (user == null)
            {
                return BadRequest("Error geting User");
            }

            var responce = user.Select(opt => new UserResponce(opt.Id, opt.Email, opt.PasswordHash, opt.PhoneNumber, opt.FirstName, opt.LastName));
            if (responce != null)
            {
                return Ok(responce);
            }

            return BadRequest("error");
        }



    }
}
