using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Data.Dtos;
using UserApi.Data.Request;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser(CreateUserDto createDto)
        {
            Result result = _userService.RegisterUser(createDto);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpGet("active-account")]
        public IActionResult ActiveAccount([FromQuery] ActiveAccountRequest request)
        {
            Result result = _userService.ActiveAccount(request);
            if (result.IsFailed) { return StatusCode(500); }
            return Ok(result.Successes);
        }

        [HttpPost("resend-email-token")]
        public async Task<IActionResult> ReSendEmailToken([FromBody] ReSendEmailActiveAccountRequest request)
        {
            Result result = await _userService.ReSendEmailActiveAccount(request);
            if (result.IsFailed) { return StatusCode(500); }
            return Ok(result.Successes);
        }
    }
}
