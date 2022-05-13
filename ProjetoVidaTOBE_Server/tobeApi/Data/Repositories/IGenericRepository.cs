using FluentResults;
using System.Collections.Generic;

namespace tobeApi.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        public T Create(T model);
        public T Update(T model, long id);
        public Result Delete(long id);
        public T Get(long id);
        public List<T> GetAll();
    }
}
