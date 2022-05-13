using AutoMapper;
using tobeApi.Data.Dtos.Student;
using tobeApi.Models;

namespace tobeApi.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, ReadStudentDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}
