using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using tobeApi.Data.Dtos.Student;
using tobeApi.Data.Repositories.Students;
using tobeApi.Models;

namespace tobeApi.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        private readonly StudentLoginService _studentLoginService;

        public StudentService(IStudentRepository repository, IMapper mapper, StudentLoginService studentLoginService)
        {
            this._repository = repository;
            this._mapper = mapper;
            _studentLoginService = studentLoginService;
        }

        public IEnumerable<ReadStudentDto> GetAll()
        {
            List<Student> students = _repository.GetAll();
            if (students == null) return null;
            return _mapper.Map<List<ReadStudentDto>>(students);
        }

        public ReadStudentDto Get(long id)
        {
            Student student = _repository.Get(id);
            if (student == null) return null;
            return _mapper.Map<ReadStudentDto>(student);
        }

        public ReadStudentDto Create(StudentDto studentDto)
        {
            Student student = _repository.Create(_mapper.Map<Student>(studentDto));
            if (student == null) return null;
            _studentLoginService.CreateStudentLoginUser(student).ContinueWith(Task =>
            {
                Console.WriteLine("Saving student how user in the api");
            });
            return _mapper.Map<ReadStudentDto>(student);
        }

        public ReadStudentDto Update(StudentDto studentDto, long id)
        {
            Student student = _repository.Update(_mapper.Map<Student>(studentDto), id);
            if (student == null) return null;
            return _mapper.Map<ReadStudentDto>(student);
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
