using mobiBooking.Data.Model;
using mobiBooking.Data.Model.Users;
using mobiBooking.Repository.Base;

namespace mobiBooking.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User FindByEmailAndPassword(string email, string password);
    }
}
