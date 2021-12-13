using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class AlternativaExercicio
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public bool AlternativaCorreta { get; set; }
        public long ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; }
    }
}
