using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Skills;
using tobeApi.Data.Repositories.Skills;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class SkillService
    {
        private readonly ISkillRepository _repository;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ReadSkillsDto> GetAll()
        {
            List<Skill> skills = _repository.GetAll();
            if (skills == null) return null;
            return _mapper.Map<List<ReadSkillsDto>>(skills);
        }

        public ReadSkillsDto Get(long id)
        {
            Skill skill = _repository.Get(id);
            if (skill == null) return null;
            return _mapper.Map<ReadSkillsDto>(skill);
        }

        public Skill Create(CreateSkillDto skillDto)
        {
            Skill skill = _repository.Create(_mapper.Map<Skill>(skillDto));
            if (skill == null) return null;
            return _mapper.Map<Skill>(skill);
        }

        public Skill Update(UpdateSkillDto skillDto, long id)
        {
            Skill skill = _repository.Update(_mapper.Map<Skill>(skillDto), id);
            if (skill == null) return null;
            return _mapper.Map<Skill>(skill);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
