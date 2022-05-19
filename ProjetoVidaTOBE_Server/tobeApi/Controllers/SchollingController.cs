using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Schollings;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("scholling")]
    [ApiController]
    public class SchollingController : ControllerBase
    {
        private readonly SchollingService _service;

        public SchollingController(SchollingService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Scholling> scholling = _service.GetAll();
            if (scholling == null) return NotFound();
            return Ok(scholling);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Scholling scholling = _service.Get(id);
            if (scholling == null) return NotFound();
            return Ok(scholling);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] SchollingDto schollingDto)
        {
            Scholling scholling = _service.Create(schollingDto);
            if (scholling == null) return BadRequest();
            return Ok(scholling);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] SchollingDto schollingDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Scholling scholling = _service.Update(schollingDto, id);
            if (scholling == null) return BadRequest();
            return Ok(scholling);
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
