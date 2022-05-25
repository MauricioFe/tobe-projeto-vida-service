using AutoMapper;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class StudentProfilesProfile : Profile
    {
        public StudentProfilesProfile()
        {
            CreateMap<Profiles, ProfileDto>();
            CreateMap<ProfileDto, Profiles>();
        }
    }
}
