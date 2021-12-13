using ProjetoVidaTOBE_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Repositories.Contracts
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Usuario Login(Usuario usuario);
    }
}
