using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tobeApi.Data.Dtos.CalendarEvent;
using tobeApi.Services;

namespace tobeApi.Controllers
{
    [Route("calendar-event")]
    [ApiController]
    public class CalendarEventController : ControllerBase
    {
        private readonly CalendarEventService _service;

        public CalendarEventController(CalendarEventService service)
        {
            this._service = service;
        }

        [Authorize(Roles = "aluno")]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ReadCalendarEventDto> readDto = _service.GetAll();
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize(Roles = "aluno")]
        [HttpGet("student/{id}")]
        public IActionResult GetCalendarEventsByStudent(long id)
        {
            IEnumerable<ReadCalendarEventDto> readDto = _service.GetCalendarEventsByStudent(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [Authorize(Roles = "aluno")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            ReadCalendarEventDto readDto = _service.Get(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        [HttpPost]
        [Authorize(Roles = "aluno")]
        public IActionResult Create([FromBody] CreateCalendarEventDto calendarEventDto)
        {
            ReadCalendarEventDto readDto = _service.Create(calendarEventDto);
            if (readDto == null) return BadRequest();
            return Ok(readDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "aluno")]
        public IActionResult Update([FromBody] UpdateCalendarEventDto calendarEventDto, long id)
        {
            ReadCalendarEventDto readDto = _service.Update(calendarEventDto, id);
            if (readDto == null) return BadRequest();
            return Ok(readDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "aluno")]
        public IActionResult Delete(long id)
        {
            Result result = _service.Delete(id);
            if (result.IsFailed) return BadRequest();
            return Ok();
        }
    }
}
