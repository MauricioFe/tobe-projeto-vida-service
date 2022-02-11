using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Dtos;
using UserApi.Data.Request;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("register-user")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private RegisterService _registerService;

        public RegisterUserController(RegisterService registerService)
        {
            this._registerService = registerService;
        }

        [HttpPost]
        public IActionResult RegisterUser(CreateUserDto createDto)
        {
            Result result = _registerService.RegisterUser(createDto);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpGet("active-account")]
        public IActionResult ActiveAccount([FromQuery] ActiveAccountRequest request)
        {
            Result result = _registerService.ActiveAccount(request);
            if (result.IsFailed) { return StatusCode(500); }
            return Ok(result.Successes);
        }

    }
}
