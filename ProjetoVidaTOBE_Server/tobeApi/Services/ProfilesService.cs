using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Profile;
using tobeApi.Data.Repositories.Profile;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<Profiles> GetAll()
        {
            List<Profiles> profiles = _repository.GetAll();
            if (profiles == null) return null;
            return _mapper.Map<List<Profiles>>(profiles);
        }

        public Profiles Get(long id)
        {
            Profiles profile = _repository.Get(id);
            if (profile == null) return null;
            return _mapper.Map<Profiles>(profile);
        }

        public Profiles Create(ProfilesDto profileDto)
        {
            Profiles profile = _repository.Create(_mapper.Map<Profiles>(profileDto));
            if (profile == null) return null;
            return _mapper.Map<Profiles>(profile);
        }

        public Profiles Update(ProfilesDto profileDto, long id)
        {
            Profiles profile = _repository.Update(_mapper.Map<Profiles>(profileDto), id);
            if (profile == null) return null;
            return _mapper.Map<Profiles>(profile);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
