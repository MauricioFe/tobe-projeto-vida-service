using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.CoursesEnrolled;
using tobeApi.Data.Repositories.CoursesEnrolled;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class CourseEnrolledService
    {
        private readonly ICourseEnrolledRepository _repository;
        private readonly IMapper _mapper;

        public CourseEnrolledService(ICourseEnrolledRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public IEnumerable<ReadCourseEnrolledDto> GetAll()
        {
            List<CourseEnrolled> courseEnrolleds = _repository.GetAll();
            if (courseEnrolleds == null) return null;
            return _mapper.Map<List<ReadCourseEnrolledDto>>(courseEnrolleds);
        }

        public IEnumerable<ReadCourseEnrolledDto> Get(long studentId)
        {
            List<CourseEnrolled> courseEnrolleds = _repository.Get(studentId);
            if (courseEnrolleds == null) return null;
            return _mapper.Map<List<ReadCourseEnrolledDto>>(courseEnrolleds);
        }

        public ReadCourseEnrolledDto GetByIds(long studentId, long courseId)
        {
            CourseEnrolled courseEnrolled = _repository.GetByIds(studentId, courseId);
            if (courseEnrolled == null) return null;
            return _mapper.Map<ReadCourseEnrolledDto>(courseEnrolled);
        }

        public Result Create(CreateCourseEnrolledDto courseEnrolledDto)
        {
            return _repository.Create(_mapper.Map<CourseEnrolled>(courseEnrolledDto));
        }

        public Result UpdateStatus(long studentId, long courseId)
        {
            return _repository.UpdateStatus(studentId, courseId);
        }
        public Result UnrolledCourse(long studentId, long courseId)
        {
            return _repository.UnrolledCourse(studentId, courseId);
        }
    }
}
