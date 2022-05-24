using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.StudentProfiles;
using tobeApi.Data.Repositories.StudentProfiles;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class StudentProfileService
    {
        private readonly IStudentProfileRepository _repository;
        private readonly IMapper _mapper;

        public StudentProfileService(IStudentProfileRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<StudentProfile> GetAll()
        {
            List<StudentProfile> profiles = _repository.GetAll();
            if (profiles == null) return null;
            return _mapper.Map<List<StudentProfile>>(profiles);
        }

        public StudentProfile Get(long id)
        {
            StudentProfile profile = _repository.Get(id);
            if (profile == null) return null;
            return _mapper.Map<StudentProfile>(profile);
        }

        public StudentProfile Create(StudentProfileDto profileDto)
        {
            StudentProfile profile = _repository.Create(_mapper.Map<StudentProfile>(profileDto));
            if (profile == null) return null;
            return _mapper.Map<StudentProfile>(profile);
        }

        public StudentProfile Update(StudentProfileDto profileDto, long id)
        {
            StudentProfile profile = _repository.Update(_mapper.Map<StudentProfile>(profileDto), id);
            if (profile == null) return null;
            return _mapper.Map<StudentProfile>(profile);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
