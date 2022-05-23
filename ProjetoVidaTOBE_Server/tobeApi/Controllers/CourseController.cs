using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Courses;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _service;

        public CourseController(CourseService service)
        {
            this._service = service;
        }


        [HttpGet("land")]
        public IActionResult GetAllCoursesLandPage()
        {
            IEnumerable<ReadLandPageCourseDto> readDto = _service.GetAllCoursesLandPage();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ReadCourseDto> readDto = _service.GetAll();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            ReadCourseDto readDto = _service.Get(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] CreateCourseDto courseDto)
        {
            ReadCourseDto readDto = _service.Create(courseDto);
            if (readDto == null) return BadRequest();
            return Ok(readDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] UpdateCourseDto courseDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            ReadCourseDto readDto = _service.Update(courseDto, id);
            if (readDto == null) return BadRequest();
            return Ok(readDto);
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

        [HttpPatch("toggle-active/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult ToggleActive(long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            Result result = _service.ToggleActive(id);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }
    }
}
