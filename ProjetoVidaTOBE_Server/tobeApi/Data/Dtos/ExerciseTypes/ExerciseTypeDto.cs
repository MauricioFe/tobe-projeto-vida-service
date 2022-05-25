using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tobeApi.Data.Dtos.ExerciseTypes
{
    public class ExerciseTypeDto
    {
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
    }
}
