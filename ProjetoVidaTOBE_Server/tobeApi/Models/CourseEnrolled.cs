namespace tobeApi.Models
{
    public class CourseEnrolled
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public string Status { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
