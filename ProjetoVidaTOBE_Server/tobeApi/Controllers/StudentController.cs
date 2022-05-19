﻿using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Students;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentController(StudentService service)
        {
            this._service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ReadStudentDto> readDto = _service.GetAll();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            ReadStudentDto readDto = _service.Get(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromBody] StudentDto studentDto)
        {
            ReadStudentDto readDto = _service.Create(studentDto);
            if (readDto == null) return BadRequest();
            return Ok(readDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Update([FromBody] StudentDto studentDto, long id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            ReadStudentDto readDto = _service.Update(studentDto, id);
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
