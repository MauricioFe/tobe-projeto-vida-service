using System.Collections.Generic;
using tobeApi.Models;

namespace tobeApi.Data.Repositories.CalendarEvents

{
    public interface ICalendarEventRepository : IGenericRepository<CalendarEvent>
    {
        List<CalendarEvent> GetCalendarEventsByStudent(long studentId);
    }
}
