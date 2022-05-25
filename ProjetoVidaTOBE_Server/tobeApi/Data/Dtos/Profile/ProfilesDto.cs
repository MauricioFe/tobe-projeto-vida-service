using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Profile 
{
    public class ProfilesDto
    {
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
    }
}
