using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Scholling;
using tobeApi.Data.Repositories.Schollings;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class SchollingService
    {
        private readonly ISchollingRepository _repository;
        private readonly IMapper _mapper;

        public SchollingService(ISchollingRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<Scholling> GetAll()
        {
            List<Scholling> schollings = _repository.GetAll();
            if (schollings == null) return null;
            return _mapper.Map<List<Scholling>>(schollings);
        }

        public Scholling Get(long id)
        {
            Scholling scholling = _repository.Get(id);
            if (scholling == null) return null;
            return _mapper.Map<Scholling>(scholling);
        }

        public Scholling Create(SchollingDto schollingDto)
        {
            Scholling scholling = _repository.Create(_mapper.Map<Scholling>(schollingDto));
            if (scholling == null) return null;
            return _mapper.Map<Scholling>(scholling);
        }

        public Scholling Update(SchollingDto schollingDto, long id)
        {
            Scholling scholling = _repository.Update(_mapper.Map<Scholling>(schollingDto), id);
            if (scholling == null) return null;
            return _mapper.Map<Scholling>(scholling);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
