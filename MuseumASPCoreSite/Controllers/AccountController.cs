﻿using Microsoft.AspNetCore.Identity;
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


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserResponce user)
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
                    PasswordHash = userModel.PasswordHash,
                    PhoneNumber = userModel.PhoneNumber,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,

                    UserName = userModel.Email
                };

               var result = await _userManager.CreateAsync(userEntity);
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
