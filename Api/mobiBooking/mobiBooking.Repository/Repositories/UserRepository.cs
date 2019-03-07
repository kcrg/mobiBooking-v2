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

        public bool CheckPermission(int id, string permission)
        {

            var query = from Users in DBContext.Users
                        join UserType in DBContext.UserType
                        on Users.UserTypeId equals UserType.Id
                        join RoleToUserTypes in DBContext.RoleToUserType
                        on UserType.Id equals RoleToUserTypes.UserTypeId
                        join Roles in DBContext.Roles
                        on RoleToUserTypes.RoleId equals Roles.Id
                        where Users.Id == id && Roles.Name == permission
                        select Users;

            return query.FirstOrDefault() != null;
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
