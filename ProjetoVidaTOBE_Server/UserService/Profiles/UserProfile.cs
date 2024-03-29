﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Dtos;
using UserApi.Models;

namespace UserApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, Users>().AfterMap((src, dest) => dest.Username = src.Email);
            CreateMap<Users, IdentityUser<int>>();
            CreateMap<Users, IdentityUserToBe>();
            CreateMap<ReadUserDto, IdentityUserToBe>();
            CreateMap<IdentityUserToBe, ReadUserDto>();
        }
    }
}
