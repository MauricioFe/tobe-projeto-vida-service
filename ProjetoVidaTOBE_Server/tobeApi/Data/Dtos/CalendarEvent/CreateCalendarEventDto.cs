﻿using System;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.CalendarEvent
{
    public class CreateCalendarEventDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public long StudentID { get; set; }
    }
}