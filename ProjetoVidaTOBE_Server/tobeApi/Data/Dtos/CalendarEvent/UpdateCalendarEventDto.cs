using System;

namespace tobeApi.Data.Dtos.CalendarEvent
{
    public class UpdateCalendarEventDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public long StudentID { get; set; }
    }
}
