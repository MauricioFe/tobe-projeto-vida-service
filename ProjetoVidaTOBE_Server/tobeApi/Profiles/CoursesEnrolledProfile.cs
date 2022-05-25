using AutoMapper;
using tobeApi.Data.Dtos.CoursesEnrolled;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class CoursesEnrolledProfile : Profile
    {
        public CoursesEnrolledProfile()
        {
            CreateMap<CourseEnrolled, ReadCourseEnrolledDto>();
            CreateMap<CourseEnrolled, CreateCourseEnrolledDto>();
            CreateMap<CreateCourseEnrolledDto, CourseEnrolled>();
        }
    }
}
