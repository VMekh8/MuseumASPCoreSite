﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {

            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserResponce user)
        {
            if (ModelState.IsValid)
            {
                var (userModel, error) = MuseumSite.Core.Models.User.CreateUser(user.Email, user.Password, user.PhoneNumber, user.Firstname, user.Lastname);

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                var result = await _userService.CreateUserAsync(userModel);
                if (result != Guid.Empty)
                {
                    return Ok(result);
                }

                return BadRequest("User creation Error");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}