using ProjetoVidaTOBE_Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Repositories
{
    public interface IGenericRepository<T>
    {
        public long Create(T objeto);
        public long Update(T objeto, long id);
        public long Delete(long id);
        public List<T> Get();
        public T Find(long id);
    }
}
