using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Data.Dtos
{
    public class UpdatedUsuarioDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "O campo Tipo de usuário é obrigatório")]
        public long TipoUsuarioId { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(60, ErrorMessage = "Esse campo não pode ter mais do que 60 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Preencha com um e-mail válido")]
        [StringLength(150, ErrorMessage = "Esse campo não pode ter mais do que 150 caracteres")]
        public string Email { get; set; }
    }
}
