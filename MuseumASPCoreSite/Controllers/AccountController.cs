using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<UserEntity> userManager, IUserService userService, SignInManager<UserEntity> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _configuration = configuration;
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
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                    signingCredentials: credentials
                );

                var _token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { Token = _token });
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
