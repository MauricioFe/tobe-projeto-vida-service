using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUserToBe> _signInManager;

        public LogoutService(SignInManager<IdentityUserToBe> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogoutUser()
        {
            Task resultadoIdentity = _signInManager.SignOutAsync();

            if (resultadoIdentity.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }
            return Result.Fail("Logout is failure");

        }
    }
}
