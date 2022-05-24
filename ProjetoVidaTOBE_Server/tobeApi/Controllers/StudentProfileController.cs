using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.StudentProfiles;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("profile")]
    [ApiController]
    public class StudentProfileController : ControllerBase
    {
        private readonly StudentProfileService _service;

        public StudentProfileController(StudentProfileService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<StudentProfile> profile = _service.GetAll();
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            StudentProfile profile = _service.Get(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] StudentProfileDto profileDto)
        {
            StudentProfile profile = _service.Create(profileDto);
            if (profile == null) return BadRequest();
            return Ok(profile);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] StudentProfileDto profileDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            StudentProfile profile = _service.Update(profileDto, id);
            if (profile == null) return BadRequest();
            return Ok(profile);
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
