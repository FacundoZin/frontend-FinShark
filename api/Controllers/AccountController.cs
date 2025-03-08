using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _UserManager;
        private readonly ITokenService _Tokenservice;


        public AccountController(UserManager<AppUser> usermanager, ITokenService tokenService)
        {
            _UserManager = usermanager;
            _Tokenservice = tokenService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var AppUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.Email
                };

                var createduser = await _UserManager.CreateAsync(AppUser, register.Password);

                if (createduser.Succeeded)
                {
                    var role = await _UserManager.AddToRoleAsync(AppUser, "User");

                    if (role.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = AppUser.UserName!,
                                Email = AppUser.Email!,
                                token = _Tokenservice.CreateToken(AppUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, role.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createduser.Errors);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}