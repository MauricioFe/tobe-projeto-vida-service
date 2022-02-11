using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Data.Request
{
    public class ActiveAccountRequest
    {
        [Required]
        public int UserId{ get; set; }
        [Required]
        public string ActivationCode{ get; set; }
    }
}
