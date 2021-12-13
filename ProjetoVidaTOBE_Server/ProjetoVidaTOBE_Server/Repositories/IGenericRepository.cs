using ProjetoVidaTOBE_Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Repositories
{
    public interface IGenericRepository<T>
    {
        public T Create(T objeto);
        public T Update(T objeto);
        public long Delete(long id);
        public List<T> Get();
        public T GetById(long id);
    }
}
