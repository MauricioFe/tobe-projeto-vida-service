using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Schollings
{
    public class SchollingDto
    {
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
    }
}
