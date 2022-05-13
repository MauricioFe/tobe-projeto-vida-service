using AutoMapper;
using tobeApi.Data.Dtos.Institutions;
using tobeApi.Models;

namespace tobeApi.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<Institution, InstitutionDto>();
            CreateMap<InstitutionDto, Institution>();
        }
    }
}
