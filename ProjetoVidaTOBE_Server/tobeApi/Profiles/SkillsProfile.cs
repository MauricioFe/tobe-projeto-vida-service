using AutoMapper;
using tobeApi.Data.Dtos.Skills;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class SkillsProfile : Profile
    {
        public SkillsProfile()
        {
            CreateMap<Skill, ReadSkillsDto>();
            CreateMap<ReadSkillsDto,Skill>();  
            CreateMap<Skill, CreateSkillDto>();
            CreateMap<CreateSkillDto, Skill>();
            CreateMap<Skill, UpdateSkillDto>();
            CreateMap<UpdateSkillDto, Skill>();
        }
    }
}
