using FluentResults;
using System.Collections.Generic;
using tobeApi.Models;

namespace tobeApi.Data.Repositories.Courses
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Result ToggleActive(long id);
        public List<Course> GetAllCoursesLandPage();
    }
}
