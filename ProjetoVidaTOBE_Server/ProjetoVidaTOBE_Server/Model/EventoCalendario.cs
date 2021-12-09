using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class EventoCalendario
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public DateTime Time { get; set; }
        public long AlunoId { get; set; }
        public Aluno Aluno { get; set; }
    }
}
