using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Courses;
using tobeApi.Data.Repositories.Courses;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ReadCourseDto> GetAll()
        {
            List<Course> courses = _repository.GetAll();
            if (courses == null) return null;
            return _mapper.Map<List<ReadCourseDto>>(courses);
        }
        public IEnumerable<ReadLandPageCourseDto> GetAllCoursesLandPage()
        {
            List<Course> courses = _repository.GetAllCoursesLandPage();
            if (courses == null) return null;
            return _mapper.Map<List<ReadLandPageCourseDto>>(courses);
        }

        public ReadCourseDto Get(long id)
        {
            Course course = _repository.Get(id);
            if (course == null) return null;
            return _mapper.Map<ReadCourseDto>(course);
        }

        public ReadCourseDto Create(CreateCourseDto courseDto)
        {
            Course course = _repository.Create(_mapper.Map<Course>(courseDto));
            if (course == null) return null;
            return _mapper.Map<ReadCourseDto>(course);
        }

        public ReadCourseDto Update(UpdateCourseDto courseDto, long id)
        {
            Course course = _repository.Update(_mapper.Map<Course>(courseDto), id);
            if (course == null) return null;
            return _mapper.Map<ReadCourseDto>(course);
        }
        public Result ToggleActive(long id)
        {
            return _repository.ToggleActive(id);
        }
        public Result Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
