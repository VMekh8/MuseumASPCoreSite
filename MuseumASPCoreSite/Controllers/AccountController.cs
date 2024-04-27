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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountController(UserManager<UserEntity> userManager,IUserService userService, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]UserResponce user)
        {
            if (ModelState.IsValid)
            {
                var (userModel, error) = MuseumSite.Core.Models.User.CreateUser(user.Email, user.Password, user.PhoneNumber, user.Firstname, user.Lastname);

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                var userEntity = new UserEntity()
                {
                    Email = userModel.Email,
                    PhoneNumber = userModel.PhoneNumber,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    UserName = userModel.Email
                };

               var result = await _userManager.CreateAsync(userEntity, user.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
