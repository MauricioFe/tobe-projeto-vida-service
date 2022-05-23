﻿using System;

namespace tobeApi.Models
{
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Active { get; set; }
    }
}
