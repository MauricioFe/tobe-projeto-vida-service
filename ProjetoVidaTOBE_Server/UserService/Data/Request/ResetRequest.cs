using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Data.Request
{
    public class ResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
