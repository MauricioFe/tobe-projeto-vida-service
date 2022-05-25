using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.ExerciseTypes;
using tobeApi.Data.Repositories.ExerciseTypes;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class ExerciseTypeService
    {
        private readonly IExerciseTypeRepository _repository;
        private readonly IMapper _mapper;

        public ExerciseTypeService(IExerciseTypeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ExerciseType> GetAll()
        {
            List<ExerciseType> exerciseTypes = _repository.GetAll();
            if (exerciseTypes == null) return null;
            return exerciseTypes;
        }

        public ExerciseType Get(long id)
        {
            ExerciseType exerciseType = _repository.Get(id);
            if (exerciseType == null) return null;
            return exerciseType;
        }

        public ExerciseType Create(ExerciseTypeDto exerciseTypeDto)
        {
            ExerciseType exerciseType = _repository.Create(_mapper.Map<ExerciseType>(exerciseTypeDto));
            if (exerciseType == null) return null;
            return _mapper.Map<ExerciseType>(exerciseType);
        }

        public ExerciseType Update(ExerciseTypeDto exerciseTypeDto, long id)
        {
            ExerciseType exerciseType = _repository.Update(_mapper.Map<ExerciseType>(exerciseTypeDto), id);
            if (exerciseType == null) return null;
            return _mapper.Map<ExerciseType>(exerciseType);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
