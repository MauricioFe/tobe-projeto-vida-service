using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Exercises;
using tobeApi.Data.Repositories.Exercises;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class ExerciseService
    {
        private readonly IExerciseRepository _repository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ReadExerciseDto> GetAll()
        {
            List<Exercise> exercises = _repository.GetAll();
            if (exercises == null) return null;
            return _mapper.Map<List<ReadExerciseDto>>(exercises);
        }

        public ReadExerciseDto Get(long id)
        {
            Exercise exercise = _repository.Get(id);
            if (exercise == null) return null;
            return _mapper.Map<ReadExerciseDto>(exercise);
        }

        public ReadExerciseDto Create(CreateExerciseDto exerciseDto)
        {
            Exercise exercise = _repository.Create(_mapper.Map<Exercise>(exerciseDto));
            if (exercise == null) return null;
            return _mapper.Map<ReadExerciseDto>(exercise);
        }

        public ReadExerciseDto Update(UpdateExerciseDto exerciseDto, long id)
        {
            Exercise exercise = _repository.Update(_mapper.Map<Exercise>(exerciseDto), id);
            if (exercise == null) return null;
            return _mapper.Map<ReadExerciseDto>(exercise);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
