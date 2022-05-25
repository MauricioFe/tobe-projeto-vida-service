using AutoMapper;
using tobeApi.Data.Dtos.Schollings;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class SchollingProfile : Profile
    {
        public SchollingProfile()
        {
            CreateMap<Scholling, SchollingDto>();
            CreateMap<SchollingDto, Scholling>();
        }
    }
}
