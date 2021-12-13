using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoVidaTOBE_Server.Model;
using ProjetoVidaTOBE_Server.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repo;
        public UsuariosController(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _repo.Get();
        }
    }
}
