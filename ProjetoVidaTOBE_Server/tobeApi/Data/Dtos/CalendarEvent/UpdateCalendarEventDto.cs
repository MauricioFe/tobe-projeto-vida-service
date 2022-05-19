using System;

namespace tobeApi.Data.Dtos.CalendarEvent
{
    public class UpdateCalendarEventDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public long StudentID { get; set; }
    }
}
