using tobeApi.Models;

namespace tobeApi.Data.Repositories.CoursesEnrolled
{
    public class CourseEnrolledRepositoryUtils
    {
        public static string getNewStatus(CourseEnrolled course, string status)
        {
            switch (course.Status)
            {
                case "0":
                    status = "1";
                    break;
                case "1":
                    status = "2";
                    break;
            }

            return status;
        }
    }
}
