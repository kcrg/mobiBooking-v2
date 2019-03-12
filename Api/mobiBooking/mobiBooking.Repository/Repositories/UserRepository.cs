using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System.Collections.Generic;
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
            IQueryable<User> query = from Users in DBContext.Users
                                     where Users.Email == email
                                     select Users;

            return query.FirstOrDefault();
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            IQueryable<User> query = from Users in DBContext.Users
                                     where Users.Email == email && Users.Password == password
                                     select Users;

            return query.FirstOrDefault();
        }

        public IEnumerable<User> FindRange(IEnumerable<int> Ids)
        {
            IQueryable<User> query = from Users in DBContext.Users
                                     where Ids.Contains(Users.Id)
                                     select Users;

            return query.AsEnumerable();
        }

        public bool UserExist(string email, string userName)
        {
            IQueryable<User> query = from Users in DBContext.Users
                                     where Users.Email == email || Users.UserName == userName
                                     select Users;

            return query.FirstOrDefault() != null;
        }
    }
}
