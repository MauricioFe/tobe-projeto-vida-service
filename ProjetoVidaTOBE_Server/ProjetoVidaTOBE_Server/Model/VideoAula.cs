using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Model
{
    public class VideoAula
    {
        public long Id { get; set; }
        public string TituloVideo { get; set; }
        public string NomeArquivo { get; set; }
        public string Link { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Descricao { get; set; }
        public string Transcricao { get; set; }
    }
}
