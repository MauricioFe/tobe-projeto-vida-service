using System;

namespace tobeApi.Models
{
    public class CalendarEvent
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
    }
}
