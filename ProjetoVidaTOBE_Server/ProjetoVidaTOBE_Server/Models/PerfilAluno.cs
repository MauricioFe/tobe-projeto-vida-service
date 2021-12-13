using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class PerfilAluno
    {
        public long PerfilId { get; set; }
        public long AlunosId { get; set; }
        public Aluno Aluno { get; set; }
        public Perfil Perfil { get; set; }
    }
}
