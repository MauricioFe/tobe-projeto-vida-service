using System;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.CalendarEvent
{
    public class CreateCalendarEventDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public long StudentID { get; set; }
    }
}
