using AutoMapper;
using tobeApi.Data.Dtos.StudentProfiles;
using tobeApi.Models;

namespace tobeApi.Profiles
{
    public class StudentProfilesProfile : Profile
    {
        public StudentProfilesProfile()
        {
            CreateMap<StudentProfile, StudentProfileDto>();
            CreateMap<StudentProfileDto, StudentProfile>();
        }
    }
}
