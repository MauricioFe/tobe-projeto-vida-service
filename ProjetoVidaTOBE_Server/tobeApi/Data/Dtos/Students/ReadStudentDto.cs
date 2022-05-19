﻿using tobeApi.Models;

namespace tobeApi.Data.Dtos.Students
{
    public class ReadStudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Level { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
        public int Active { get; set; }
        public Institution Institution { get; set; }
        public Scholling scholling { get; set; }
    }
}
