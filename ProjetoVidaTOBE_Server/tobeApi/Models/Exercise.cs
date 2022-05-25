using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tobeApi.Models
{
    public class Exercise
    {
        public long Id { get; set; }
        public string Enumeration { get; set; }
        public string Question { get; set; }
        public string SuggestedAnswer { get; set; }
        public long ProfilesId { get; set; }
        public long ExerciseTypeId { get; set; }
        public Profiles Profiles { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
