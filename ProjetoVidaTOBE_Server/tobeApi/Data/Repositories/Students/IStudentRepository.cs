using FluentResults;
using System.Collections.Generic;
using tobeApi.Models;

namespace tobeApi.Data.Repositories.Students
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Result ToggleActive(long id);
    }
}
