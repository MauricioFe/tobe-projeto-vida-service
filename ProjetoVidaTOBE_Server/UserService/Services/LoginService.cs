using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Request;
using UserApi.Models;

namespace UserApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUserToBe> _signInManager;
        private TokenService _tokenService;

        public LoginService(TokenService tokenService, SignInManager<IdentityUserToBe> signInManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public Result SignInUser(LoginRequest request)
        {
            var identityResult = _signInManager
                .PasswordSignInAsync(request.Email, request.Password, false, false);
            if (identityResult.Result.Succeeded)
            {
                var idetityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user => user.NormalizedEmail == request.Email.ToUpper());
                Token token = _tokenService
                    .CreateToken(idetityUser);

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Error in processing the signin");

        }

        public Result ResetPassword(ResetPasswordRequest request)
        {
            IdentityUserToBe identityUser = GetUserByEmail(request.Email);
            IdentityResult identityResult = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok().WithSuccess("Reset Password complete");
            }
            return Result.Fail("Error at reset password");
        }

        public Result RequestResetPassword(ResetRequest request)
        {
            IdentityUserToBe identityUser = GetUserByEmail(request.Email);
            if (identityUser != null)
            {
                string resetPasswordCode = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(resetPasswordCode);
            }
            return Result.Fail("User not found");
        }

        private IdentityUserToBe GetUserByEmail(string email)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
