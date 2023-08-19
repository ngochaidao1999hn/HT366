using HT366.Application.Dtos.Identity;
using HT366.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT366.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var res = await _userService.Authorize(dto);
            if (res.AccessToken is null)
            {
                return BadRequest("Email/Password not correct");
            }
            return Ok(res);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            { 
                await _userService.Register(dto);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
