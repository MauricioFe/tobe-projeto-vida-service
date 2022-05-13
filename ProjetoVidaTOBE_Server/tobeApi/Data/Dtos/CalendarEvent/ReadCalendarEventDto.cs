using System;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.CalendarEvent
{
    public class ReadCalendarEventDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Student Student { get; set; }
    }
}
