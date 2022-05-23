using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.CoursesEnrolled;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("course-enrolled")]
    [ApiController]
    public class CourseEnrolledController : ControllerBase
    {
        private readonly CourseEnrolledService _service;

        public CourseEnrolledController(CourseEnrolledService service)
        {
            this._service = service;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ReadCourseEnrolledDto> readDto = _service.GetAll();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize(Roles = "aluno")]
        [HttpGet("{idStudent}")]
        public IActionResult Get(long idStudent)
        {
            IEnumerable<ReadCourseEnrolledDto> readDto = _service.Get(idStudent);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize(Roles = "aluno")]
        [HttpGet("{idStudent}/{idCourse}")]
        public IActionResult GetByIds(long idStudent, long idCourse)
        {
            ReadCourseEnrolledDto readDto = _service.GetByIds(idStudent, idCourse);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        [Authorize(Roles = "aluno")]
        [HttpPost]
        public IActionResult Create([FromBody] CreateCourseEnrolledDto courseEnrolledDto)
        {
            Result result = _service.Create(courseEnrolledDto);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "aluno")]
        [HttpPatch("{idStudent}/{idCourse}")]
        public IActionResult UpdateStatus(long idStudent, long idCourse)
        {
            if (_service.GetByIds(idStudent, idCourse) == null)
            {
                return NotFound();
            }
            Result result = _service.UpdateStatus(idStudent, idCourse);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "aluno")]
        [HttpDelete("{idStudent}/{idCourse}")]
        public IActionResult Delete(long idStudent, long idCourse)
        {
            if (_service.GetByIds(idStudent, idCourse) == null)
            {
                return NotFound();
            }
            Result result = _service.UnrolledCourse(idStudent, idCourse);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }
    }
}
