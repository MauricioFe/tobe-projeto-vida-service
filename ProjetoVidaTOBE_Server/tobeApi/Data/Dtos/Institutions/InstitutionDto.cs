using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Institutions
{
    public class InstitutionDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
