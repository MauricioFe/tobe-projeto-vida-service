using System.ComponentModel.DataAnnotations;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.Skills
{
    public class ReadSkillsDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public StudentProfile StudentProfile { get; set; }
    }
}
