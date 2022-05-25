using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.ExerciseTypes;
using tobeApi.Models;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("exercise-type")]
    [ApiController]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly ExerciseTypeService _service;

        public ExerciseTypeController(ExerciseTypeService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ExerciseType> exerciseType = _service.GetAll();
            if (exerciseType == null) return NotFound();
            return Ok(exerciseType);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            ExerciseType exerciseType = _service.Get(id);
            if (exerciseType == null) return NotFound();
            return Ok(exerciseType);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] ExerciseTypeDto exerciseTypeDto)
        {
            ExerciseType exerciseType = _service.Create(exerciseTypeDto);
            if (exerciseType == null) return BadRequest();
            return Ok(exerciseType);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] ExerciseTypeDto exerciseTypeDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            ExerciseType exerciseType = _service.Update(exerciseTypeDto, id);
            if (exerciseType == null) return BadRequest();
            return Ok(exerciseType);
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
