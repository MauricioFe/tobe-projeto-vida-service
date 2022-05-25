using AutoMapper;
using tobeApi.Data.Dtos.Courses;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, ReadCourseDto>();
            CreateMap<Course, ReadLandPageCourseDto>();
            CreateMap<Course, CreateCourseDto>();
            CreateMap<Course, UpdateCourseDto>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
        }
    }
}
