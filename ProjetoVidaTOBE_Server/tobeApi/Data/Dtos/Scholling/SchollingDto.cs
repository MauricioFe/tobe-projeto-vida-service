using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Scholling
{
    public class SchollingDto
    {
        [Required]
        [StringLength(45)]
        public string Name { get; set; }
    }
}
