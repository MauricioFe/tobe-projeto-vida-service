using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Exercises
{
    public class UpdateExerciseDto
    {
        [Required]
        [StringLength(30)]
        public string Enumeration { get; set; }
        [Required]
        [StringLength(200)]
        public string Question { get; set; }
        [Required]
        [StringLength(150)]
        public string SuggestedAnswer { get; set; }
        [Required]
        public long ProfilesId { get; set; }
        [Required]
        public long ExerciseTypeId { get; set; }
    }
}
