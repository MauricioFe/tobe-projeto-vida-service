using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Profile;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileService _service;

        public ProfilesController(ProfileService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Models.Profiles> profile = _service.GetAll();
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Models.Profiles profile = _service.Get(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] ProfilesDto profileDto)
        {
            Models.Profiles profile = _service.Create(profileDto);
            if (profile == null) return BadRequest();
            return Ok(profile);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] ProfilesDto profileDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Models.Profiles profile = _service.Update(profileDto, id);
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
