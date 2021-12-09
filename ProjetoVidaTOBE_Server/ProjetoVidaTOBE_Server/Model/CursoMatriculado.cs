using ProjetoVidaTOBE_Server.Utils.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class CursoMatriculado
    {
        public long AlunoId { get; set; }
        public long CursoId { get; set; }
        public StatusCursoMatriculado StatusCursoMatriculado { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
    }
}
