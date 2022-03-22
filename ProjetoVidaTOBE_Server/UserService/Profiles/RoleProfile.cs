using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Dtos;

namespace UserApi.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole<int>, ReadRoleDto>();
            CreateMap<ReadRoleDto, IdentityRole<int>>();
            CreateMap<IdentityRole<int>, UpdateRoleDto>();
            CreateMap<UpdateRoleDto, IdentityRole<int>>();
            CreateMap<IdentityRole<int>, CreateRoleDto>();
            CreateMap<CreateRoleDto, IdentityRole<int>>();
        }
    }
}
