using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Base
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
