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
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUserToBe> _userManager;
        private EmailService _emailService;
        public RegisterService(UserManager<IdentityUserToBe> userManager, IMapper mapper, EmailService emailService)
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
            //_userManager.AddToRoleAsync(usuarioIdentity, "regular");
            if (resultadoIdentity.Result.Succeeded)
            {
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.SendEmail(new[] { userIdentity.Email },
                    "Link de Ativação", userIdentity.Id, encodedCode, userIdentity.UserName);

                return Result.Ok();
            }
            return Result.Fail("Register user failure");

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
