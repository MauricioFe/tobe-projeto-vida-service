using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UserApi.Data.Request;
using UserApi.Models;

namespace UserApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUserToBe> _signInManager;
        private TokenService _tokenService;
        private EmailService _emailService;
        private UserManager<IdentityUserToBe> _userManager;

        public LoginService(TokenService tokenService, SignInManager<IdentityUserToBe> signInManager, EmailService emailService, UserManager<IdentityUserToBe> userManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _userManager = userManager;
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
                    .CreateToken(idetityUser, _signInManager.UserManager.GetRolesAsync(idetityUser).Result.FirstOrDefault());

                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Error in processing the signin");

        }

        internal string ConfirmResetPasswordByEmail(ConfirmResetPasswordRequest request)
        {
            var identityUser = _userManager.Users
                .FirstOrDefault(u => u.Id == request.UserId);
            if (identityUser != null)
            {
                return request.ActivationCode;
            }
            return null;
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
                try
                {
                    GenerateEmailResetSenha(identityUser);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Generate Email to reset password fail:\n{ex.Message}");
                    return Result.Fail($"Generate Email to reset password fail:\n{ex.Message}");
                }
            }
            return Result.Fail("User not found");
        }

        private IdentityUserToBe GetUserByEmail(string email)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }

        private Result GenerateEmailResetSenha(IdentityUserToBe user)
        {
            try
            {
                string resetPasswordCode = _signInManager.UserManager.GeneratePasswordResetTokenAsync(user).Result;
                var encodedCode = HttpUtility.UrlEncode(resetPasswordCode);
                _emailService.SendEmailResetPassword(new[] { user.Email }, "Recuperação de senha ToBe", user.Id, encodedCode, user.Email, user.FullName);
                return Result.Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
