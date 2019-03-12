using System.Collections.Generic;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;

namespace mobiBooking.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User FindByEmailAndPassword(string email, string password);
        bool UserExist(string email, string userName);
        User FindByEmail(string email);
        IEnumerable<User> FindRange(IEnumerable<int> Ids);
    }
}
