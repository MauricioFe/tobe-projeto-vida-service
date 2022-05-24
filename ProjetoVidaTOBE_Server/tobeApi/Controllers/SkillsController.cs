using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Skills;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _service;

        public SkillController(SkillService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ReadSkillsDto> skills = _service.GetAll();
            if (skills == null) return NotFound();
            return Ok(skills);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            ReadSkillsDto skills = _service.Get(id);
            if (skills == null) return NotFound();
            return Ok(skills);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] CreateSkillDto skillsDto)
        {
            Skill skills = _service.Create(skillsDto);
            if (skills == null) return BadRequest();
            return Ok(skills);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] UpdateSkillDto skillsDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Skill skills = _service.Update(skillsDto, id);
            if (skills == null) return BadRequest();
            return Ok(skills);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Result result = _service.Delete(id);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }
    }
}
