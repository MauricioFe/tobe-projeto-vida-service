using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Aluno
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public string Nivel { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public long InstituicaoId { get; set; }
        public long EscolaridadeId { get; set; }
        public bool Ativo { get; set; }
        public Instituicao Instituicao { get; set; }
        public Escolaridade Escolaridade { get; set; }
    }
}
