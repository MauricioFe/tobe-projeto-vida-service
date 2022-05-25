using System.ComponentModel.DataAnnotations;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.Skills
{
    public class ReadSkillsDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Models.Profiles StudentProfile { get; set; }
    }
}
