using System;

namespace tobeApi.Data.Dtos.Courses
{
    public class UpdateCourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
