using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Skills
{
    public class CreateSkillDto
    {
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
        [Required]
        public long ProfilesID{ get; set; }
    }
}
