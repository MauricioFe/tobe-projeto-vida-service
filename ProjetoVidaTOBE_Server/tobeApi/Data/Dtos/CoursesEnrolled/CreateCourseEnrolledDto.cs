using System;

namespace tobeApi.Data.Dtos.CoursesEnrolled
{
    public class CreateCourseEnrolledDto
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public string Status { get; set; }
    }
}
