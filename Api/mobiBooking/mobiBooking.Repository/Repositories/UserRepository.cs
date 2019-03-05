using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;

namespace mobiBooking.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {

        }
    }
}
