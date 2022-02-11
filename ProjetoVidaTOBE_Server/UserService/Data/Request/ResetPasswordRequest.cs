using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Data.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
