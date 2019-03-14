using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {

        }

        public Task<IEnumerable<User>> FindAllWithout(int loggedUserid)
        {
            return Task.Run(() => (from Users in DBContext.Users
                                   where Users.Id != loggedUserid
                                   select Users).AsEnumerable());
        }

        public Task<User> FindByEmail(string email)
        {
            return (from Users in DBContext.Users
                    where Users.Email == email
                    select Users)
                    .ToAsyncEnumerable()
                    .FirstOrDefault();

        }

        public Task<User> FindByEmailAndPassword(string email, string password)
        {
            return (from Users in DBContext.Users
                    where Users.Email == email && Users.Password == password
                    select Users)
                    .ToAsyncEnumerable()
                    .FirstOrDefault();

        }

        public Task<IEnumerable<User>> FindRange(IEnumerable<int> Ids)
        {
            return Task.Run(() => (from Users in DBContext.Users
                                   where Ids.Contains(Users.Id)
                                   select Users).AsEnumerable());

        }

        public async Task<bool> UserExist(string email, string userName)
        {
            User user = await (from Users in DBContext.Users
                               where Users.Email == email || Users.UserName == userName
                               select Users).ToAsyncEnumerable().FirstOrDefault();

            return user != null;
        }
    }
}
