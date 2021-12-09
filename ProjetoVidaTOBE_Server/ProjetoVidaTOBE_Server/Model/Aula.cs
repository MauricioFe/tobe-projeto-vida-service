using ProjetoVidaTOBE_Server.Utils.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Aula
    {
        public long Id { get; set; }
        public StatusAula StatusAula { get; set; }
        public bool UltimaAula { get; set; }
        public long CursoId { get; set; }
        public long videoAulaId { get; set; }
        public Curso Curso { get; set; }
        public VideoAula VideoAula { get; set; }
    }
}
