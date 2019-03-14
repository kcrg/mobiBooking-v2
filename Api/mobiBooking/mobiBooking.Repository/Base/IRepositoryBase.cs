using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Base
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> Find(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();
    }
}
