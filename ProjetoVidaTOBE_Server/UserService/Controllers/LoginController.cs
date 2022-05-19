using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Request;
using UserApi.Services;

namespace UserApi.Controllers
{

    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            Result result = _loginService.SignInUser(request);
            if (result.IsFailed) { return Unauthorized(result.Errors); }
            return Ok(new { token = result.Successes[0].Message });
        }

        [HttpPost("request-reset-password")]
        public IActionResult SolicitaResetSenhaUsuario(ResetRequest request)
        {
            Result resultado = _loginService.RequestResetPassword(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }

        [HttpGet("confirm-reset-password")]
        public IActionResult ConfirmResetPasswordByEmail([FromQuery] ConfirmResetPasswordRequest request)
        {
            var token = _loginService.ConfirmResetPasswordByEmail(request);
            if (string.IsNullOrEmpty(token)) { return StatusCode(500); }
            return Ok(new { token });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetaSenhaUsuario(ResetPasswordRequest request)
        {
            Result resultado = _loginService.ResetPassword(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
