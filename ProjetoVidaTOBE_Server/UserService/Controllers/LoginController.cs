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
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            Result result = _loginService.SignInUser(request);
            if (result.IsFailed) { return Unauthorized(result.Errors); }
            return Ok(result.Successes);
        }

        [HttpPost("request-reset-password")]
        public IActionResult SolicitaResetSenhaUsuario(ResetRequest request)
        {
            Result resultado = _loginService.RequestResetPassword(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
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
