using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Exercicio
    {
        public long Id { get; set; }
        public string Enumeracao { get; set; }
        public string Enunciado { get; set; }
        public string SugestaoResposta { get; set; }
        public long PerfilId { get; set; }
        public long TipoExercicioId { get; set; }
        public Perfil Perfil { get; set; }
        public TipoExercicio TipoExercicio { get; set; }
    }
}
