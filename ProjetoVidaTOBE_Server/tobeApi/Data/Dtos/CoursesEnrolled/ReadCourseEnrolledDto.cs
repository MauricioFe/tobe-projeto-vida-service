using System;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.CoursesEnrolled
{
    public class ReadCourseEnrolledDto
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public string Status { get; set; }
    }
}
