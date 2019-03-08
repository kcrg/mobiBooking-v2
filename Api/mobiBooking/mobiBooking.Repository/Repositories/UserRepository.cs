using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Data.Model.Users;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System.Linq;

namespace mobiBooking.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {

        }

        public User FindByEmailAndPassword(string email, string password)
        {
            var query = from Users in DBContext.Users
                        where Users.Email == email && Users.Password == password
                        select Users;

            return query.FirstOrDefault();
        }
    }
}
