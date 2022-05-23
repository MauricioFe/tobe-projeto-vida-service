using FluentResults;
using System.Collections.Generic;
using tobeApi.Models;

namespace tobeApi.Data.Repositories.CoursesEnrolled
{
    public interface ICourseEnrolledRepository
    {
        Result UpdateStatus(long studentId, long courseId);
        Result Create(CourseEnrolled courseEnrolled);
        List<CourseEnrolled> GetAll();
        List<CourseEnrolled> Get(long studentId);
        CourseEnrolled GetByIds(long studentId, long courseId);
        Result UnrolledCourse(long studentId, long CourseId);


    }
}
