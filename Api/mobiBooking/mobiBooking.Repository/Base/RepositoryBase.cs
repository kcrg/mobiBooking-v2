using mobiBooking.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MobiBookingDBContext DBContext { get; set; }

        public RepositoryBase(MobiBookingDBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        public IEnumerable<T> FindAll()
        {
            return DBContext.Set<T>();
        }

        public T Find(int id)
        {
            return DBContext.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            DBContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DBContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            DBContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            DBContext.SaveChanges();
        }
    }
}
