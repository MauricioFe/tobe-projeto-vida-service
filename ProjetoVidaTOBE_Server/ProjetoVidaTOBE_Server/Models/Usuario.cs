using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Usuario
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Esse campo é obrigatório")]
        public long TipoUsuarioId { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [StringLength(60, ErrorMessage = "Esse campo não pode ter mais do que 60 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [EmailAddress(ErrorMessage ="Preencha com um e-mail válido")]
        [StringLength(150, ErrorMessage = "Esse campo não pode ter mais do que 150 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MinLength(6, ErrorMessage = "O campo senha precisa ter no mínimo 6 dígitos")]
        [StringLength(100, ErrorMessage = "Esse campo não pode ter mais do que 100 caracteres")]
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
