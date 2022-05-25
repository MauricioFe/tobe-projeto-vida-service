using AutoMapper;
using tobeApi.Data.Dtos.CalendarEvent;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class CalendarEventProfile : Profile
    {
        public CalendarEventProfile()
        {
            CreateMap<CalendarEvent, ReadCalendarEventDto>();
            CreateMap<CalendarEvent, CreateCalendarEventDto>();
            CreateMap<CalendarEvent, UpdateCalendarEventDto>();
            CreateMap<CreateCalendarEventDto, CalendarEvent>();
            CreateMap<UpdateCalendarEventDto, CalendarEvent>();
        }
    }
}
