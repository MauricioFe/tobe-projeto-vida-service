using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Dtos;
using UserApi.Models;

namespace UserApi.Services
{
    public class RoleService
    {
        private IMapper _mapper;
        private RoleManager<IdentityRole<int>> _roleManager;

        public RoleService(IMapper mapper, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public List<ReadRoleDto> GetRoles()
        {
            List<IdentityRole<int>> roleList = _roleManager.Roles.ToList();
            if (roleList == null)
            {
                return null;
            }
            return _mapper.Map<List<ReadRoleDto>>(roleList);
        }

        public ReadRoleDto CreateRole(CreateRoleDto createRoleDto)
        {
            IdentityRole<int> role = _mapper.Map<IdentityRole<int>>(createRoleDto);
            Task<IdentityResult> result = _roleManager.CreateAsync(role);
            if (result.IsCompletedSuccessfully)
            {
                Task<IdentityRole<int>> roleCreated = _roleManager.FindByNameAsync(role.Name);
                return _mapper.Map<ReadRoleDto>(roleCreated.Result);
            }
            return null;
        }

        public Result UpdateRole(UpdateRoleDto roleDto, int id)
        {
            IdentityRole<int> role = _mapper.Map<IdentityRole<int>>(GetRoleById(id));
            if (role == null)
            {
                return Result.Fail("Role not exists");
            }
            role.Name = roleDto.Name;
            Task<IdentityResult> result = _roleManager.UpdateAsync(role);
            if (result.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }
            return Result.Fail("Erro in update");
        }
        public Result DeleteRole(int id)
        {
            IdentityRole<int> role = _mapper.Map<IdentityRole<int>>(GetRoleById(id));
            if (role == null)
            {
                return Result.Fail("Role not exists");
            }
            Task<IdentityResult> result = _roleManager.DeleteAsync(role);
            if (result.IsFaulted)
            {
                return Result.Fail("Erro in delete");
            }
            return Result.Ok();
        }


        internal ReadRoleDto GetRoleById(int id)
        {
            Task<IdentityRole<int>> role = _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return null;
            }
            return _mapper.Map<ReadRoleDto>(role.Result);
        }
    }
}
