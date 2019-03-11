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

        public User FindByEmail(string email)
        {
            var query = from Users in DBContext.Users
                        where Users.Email == email
                        select Users;

            return query.FirstOrDefault();
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            var query = from Users in DBContext.Users
                        where Users.Email == email && Users.Password == password
                        select Users;

            return query.FirstOrDefault();
        }

        public bool UserExist(string email, string userName)
        {
            var query = from Users in DBContext.Users
                        where Users.Email == email || Users.UserName == userName
                        select Users;

            return query.FirstOrDefault() != null;
        }
    }
}
