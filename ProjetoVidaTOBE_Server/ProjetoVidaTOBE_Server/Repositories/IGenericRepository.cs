using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Repositories
{
    interface IGenericRepository<T>
    {
        public T Adiciona(T t);
    }
}
