using System.ComponentModel.DataAnnotations;
using tobeApi.Models;

namespace tobeApi.Data.Dtos.Exercises
{
    public class ReadExerciseDto
    {
        public long Id { get; set; }
        public string Enumeration { get; set; }
        public string Question { get; set; }
        public string SuggestedAnswer { get; set; }
        public Profiles Profiles { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
