﻿using System.ComponentModel.DataAnnotations;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Usuario
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "O campo Tipo de usuário é obrigatório")]
        public long TipoUsuarioId { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(60, ErrorMessage = "Esse campo não pode ter mais do que 60 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="Preencha com um e-mail válido")]
        [StringLength(150, ErrorMessage = "Esse campo não pode ter mais do que 150 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MinLength(6, ErrorMessage = "O campo senha precisa ter no mínimo 6 dígitos")]
        [StringLength(100, ErrorMessage = "Esse campo não pode ter mais do que 100 caracteres")]
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}