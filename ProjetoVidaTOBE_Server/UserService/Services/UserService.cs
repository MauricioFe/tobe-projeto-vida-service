using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UserApi.Data.Dtos;
using UserApi.Data.Request;
using UserApi.Models;

namespace UserApi.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<IdentityUserToBe> _userManager;
        private EmailService _emailService;
        public UserService(UserManager<IdentityUserToBe> userManager, IMapper mapper, EmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public Result RegisterUser(CreateUserDto createDto)
        {
            Users usuario = _mapper.Map<Users>(createDto);
            IdentityUserToBe userIdentity = _mapper.Map<IdentityUserToBe>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(userIdentity, createDto.Password);
            _userManager.AddToRoleAsync(userIdentity, createDto.Role);
            if (resultadoIdentity.Result.Succeeded)
            {
                try
                {
                    GenerateEmailToken(userIdentity);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Generate Email Token fail:\n{ex.Message}");
                    return Result.Fail($"Generate Email Token fail:\n{ex.Message}");
                }
            }
            return Result.Fail("Register user failure");

        }

        public Result GenerateEmailToken(IdentityUserToBe userIdentity)
        {
            try
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.SendEmailActivateAccount(new[] { userIdentity.Email },
                    "Link de Ativação", userIdentity.Id, encodedCode, userIdentity.Email);
                return Result.Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        internal async Task<Result> ReSendEmailActiveAccount(ReSendEmailActiveAccountRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result.Fail("E-mail not exists");
            }
            try
            {
                GenerateEmailToken(user);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generate Email Token fail:\n{ex.Message}");
                return Result.Fail($"Generate Email Token fail:\n{ex.Message}");
            }

        }

        internal Result ActiveAccount(ActiveAccountRequest request)
        {
            var identityUser = _userManager.Users
                .FirstOrDefault(u => u.Id == request.UserId);
            var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ActivationCode).Result;
            if (identityResult.Succeeded) { return Result.Ok(); }
            return Result.Fail("Account activation failure");
        }
    }
}
