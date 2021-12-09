using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class Competencia
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public long PerfilId { get; set; }
        public Perfil Perfil { get; set; }
    }
}
