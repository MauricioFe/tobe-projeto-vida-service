using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Institutions;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("institution")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly InstitutionService _service;

        public InstitutionController(InstitutionService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Institution> institution = _service.GetAll();
            if (institution == null) return NotFound();
            return Ok(institution);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Institution institution = _service.Get(id);
            if (institution == null) return NotFound();
            return Ok(institution);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] InstitutionDto institutionDto)
        {
            Institution institution = _service.Create(institutionDto);
            if (institution == null) return BadRequest();
            return Ok(institution);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] InstitutionDto institutionDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Institution institution = _service.Update(institutionDto, id);
            if (institution == null) return BadRequest();
            return Ok(institution);
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
