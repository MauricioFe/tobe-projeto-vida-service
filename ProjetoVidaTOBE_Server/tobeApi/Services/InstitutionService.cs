using AutoMapper;
using FluentResults;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Institutions;
using tobeApi.Data.Dtos.Scholling;
using tobeApi.Data.Repositories.Institutions;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class InstitutionService
    {
        private readonly IInstitutionRepository _repository;
        private readonly IMapper _mapper;

        public InstitutionService(IInstitutionRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<Institution> GetAll()
        {
            List<Institution> institutions = _repository.GetAll();
            if (institutions == null) return null;
            return _mapper.Map<List<Institution>>(institutions);
        }

        public Institution Get(long id)
        {
            Institution institution = _repository.Get(id);
            if (institution == null) return null;
            return _mapper.Map<Institution>(institution);
        }

        public Institution Create(InstitutionDto institutionDto)
        {
            Institution institution = _repository.Create(_mapper.Map<Institution>(institutionDto));
            if (institution == null) return null;
            return _mapper.Map<Institution>(institution);
        }

        public Institution Update(InstitutionDto institutionDto, long id)
        {
            Institution institution = _repository.Update(_mapper.Map<Institution>(institutionDto), id);
            if (institution == null) return null;
            return _mapper.Map<Institution>(institution);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
