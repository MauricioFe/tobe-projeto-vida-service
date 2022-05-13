using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Students
{
    public class StudentDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }
        [Required]
        [StringLength(120)]
        public string Email { get; set; }
        [Required]
        [StringLength(2)]
        public string State { get; set; }
        [Required]
        [StringLength(4)]
        public string Level { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        public long InstitutionsID { get; set; }
        [Required]
        public long SchoolingID { get; set; }
    }
}
