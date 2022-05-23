using System;

namespace tobeApi.Data.Dtos.Courses
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
