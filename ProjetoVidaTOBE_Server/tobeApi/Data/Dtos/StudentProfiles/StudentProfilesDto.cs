using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.StudentProfiles
{
    public class StudentProfileDto
    {
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
    }
}
