using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Preencha com um e-mail válido")]
        [StringLength(150, ErrorMessage = "Esse campo não pode ter mais do que 150 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MinLength(6, ErrorMessage = "O campo senha precisa ter no mínimo 6 dígitos")]
        [StringLength(100, ErrorMessage = "Esse campo não pode ter mais do que 100 caracteres")]
        public string Senha { get; set; }
    }
}
