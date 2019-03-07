using mobiBooking.Data;
using mobiBooking.Data.Model.Users;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;

namespace mobiBooking.Repository.Repositories
{
    public class UserStatusRepository : RepositoryBase<UserStatus>, IUserStatusRepository
    {
        public UserStatusRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
