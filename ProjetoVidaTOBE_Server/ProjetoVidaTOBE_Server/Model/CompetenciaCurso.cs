using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class CompetenciaCurso
    {
        public long CursoId { get; set; }
        public long CompetenciaId { get; set; }
        public Competencia Competencia { get; set; }
        public Curso Curso { get; set; }
    }
}
