using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Data.Dtos;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleService _roleService;
        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        public IActionResult AddRole([FromBody] CreateRoleDto roleDto)
        {
            ReadRoleDto role = _roleService.CreateRole(roleDto);
            return CreatedAtAction(nameof(GetRoleById), new { Id = role.Id }, role);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            ReadRoleDto readDto = _roleService.GetRoleById(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);

        }
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            List<ReadRoleDto> listRoles = _roleService.GetRoles();
            if (listRoles == null) return NotFound();
            return Ok(listRoles);

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateRoleDto roleDto)
        {
            Result resultado = _roleService.UpdateRole(roleDto, id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _roleService.DeleteRole(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
