using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.HealthCare.Api.Repositories
{
    public interface IRepository<T> where T :class
    {
        Task Create(T obj);
        Task<T> Update(int id, T obj);
        Task<T> Delete(int id);
    }

    public interface IGetRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
    }
}
