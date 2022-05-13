using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.CalendarEvent;
using tobeApi.Data.Repositories.CalendarEvents;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class CalendarEventService
    {
        private readonly ICalendarEventRepository _repository;
        private readonly IMapper _mapper;

        public CalendarEventService(ICalendarEventRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ReadCalendarEventDto> GetAll()
        {
            List<CalendarEvent> students = _repository.GetAll();
            if (students == null) return null;
            return _mapper.Map<List<ReadCalendarEventDto>>(students);
        }


        public IEnumerable<ReadCalendarEventDto> GetCalendarEventsByStudent(long id)
        {
            List<CalendarEvent> students = _repository.GetCalendarEventsByStudent(id);
            if (students == null) return null;
            return _mapper.Map<List<ReadCalendarEventDto>>(students);
        }

        public ReadCalendarEventDto Get(long id)
        {
            CalendarEvent student = _repository.Get(id);
            if (student == null) return null;
            return _mapper.Map<ReadCalendarEventDto>(student);
        }

        public ReadCalendarEventDto Create(CreateCalendarEventDto studentDto)
        {
            CalendarEvent student = _repository.Create(_mapper.Map<CalendarEvent>(studentDto));
            if (student == null) return null;
            return _mapper.Map<ReadCalendarEventDto>(student);
        }

        public ReadCalendarEventDto Update(UpdateCalendarEventDto studentDto, long id)
        {
            CalendarEvent student = _repository.Update(_mapper.Map<CalendarEvent>(studentDto), id);
            if (student == null) return null;
            return _mapper.Map<ReadCalendarEventDto>(student);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
