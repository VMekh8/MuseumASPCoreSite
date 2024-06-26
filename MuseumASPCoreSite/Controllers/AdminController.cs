﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Domain.Entitites;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserService _userService;

        public AdminController(UserManager<UserEntity> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserResponse>>> GetAllUser()
        {
            var user = await _userService.GetUsersAsync();

            if (user == null)
            {
                return BadRequest("Error geting User");
            }

            var responce = user.Select(opt => new UserResponse(opt.Id, opt.Email, opt.PasswordHash, opt.PhoneNumber, opt.FirstName, opt.LastName));
            if (responce != null)
            {
                return Ok(responce);
            }

            return BadRequest("error");
        }

        [HttpPut("EditUser")]
        public async Task<ActionResult> EditUser([FromForm] UserResponse userResponce)
        {
            var userEntity = await _userManager.FindByIdAsync(userResponce.id);
            if (userEntity == null)
            {
                return NotFound("User not found");
            }

            userEntity.Email = userResponce.Email;
            userEntity.UserName = userResponce.Email;
            userEntity.PhoneNumber = userResponce.PhoneNumber;
            userEntity.FirstName = userResponce.Firstname;
            userEntity.LastName = userResponce.Lastname;

            var result = await _userManager.UpdateAsync(userEntity);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);

        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByNameAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(new UserResponse(
                user.Id,
                user.Email,
                user.PasswordHash,
                user.PhoneNumber,
                user.FirstName,
                user.LastName
                ));
        }

    }
}
