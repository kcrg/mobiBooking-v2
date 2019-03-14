using mobiBooking.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MobiBookingDBContext DBContext { get; set; }

        public RepositoryBase(MobiBookingDBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        public Task<IEnumerable<T>> FindAll()
        {
            return Task.Run<IEnumerable<T>>(() => DBContext.Set<T>());
        }

        public async Task<T> Find(int id) => await DBContext.Set<T>().FindAsync(id);

        public async Task Create(T entity)
        {
            await DBContext.Set<T>().AddAsync(entity);
        }

        public Task Update(T entity)
        {
            return Task.Run(() => DBContext.Set<T>().Update(entity));
        }

        public Task Delete(T entity)
        {
            return Task.Run(() => DBContext.Set<T>().Remove(entity));
        }

        public async Task Save()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
