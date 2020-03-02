using mobiBooking.Data;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Base
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        protected MobiBookingDBContext DBContext { get; set; }

        public RepositoryBase(MobiBookingDBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        public async Task Save()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
