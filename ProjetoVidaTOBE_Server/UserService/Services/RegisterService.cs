using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UserApi.Data.Dtos;
using UserApi.Models;

namespace UserApi.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        public RegisterService(UserManager<IdentityUser<int>> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Result RegisterUser(CreateUserDto createDto)
        {
            Users usuario = _mapper.Map<Users>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(userIdentity, createDto.Password);
            //_userManager.AddToRoleAsync(usuarioIdentity, "regular");
            if (resultadoIdentity.Result.Succeeded)
            {
                //var code = _userManager
                //    .GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                //var encodedCode = HttpUtility.UrlEncode(code);

                //_emailService.SendEmail(new[] { userIdentity.Email },
                //    "Link de Ativação", userIdentity.Id, encodedCode, userIdentity.UserName);

                return Result.Ok();
            }
            return Result.Fail("Register user is failure");

        }
    }
}
