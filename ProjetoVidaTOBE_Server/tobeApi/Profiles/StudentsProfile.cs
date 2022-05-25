﻿using AutoMapper;
using tobeApi.Data.Dtos.Students;
using tobeApi.Models;

namespace tobeApi.ProfilesMapper
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
            CreateMap<Student, ReadStudentDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}
