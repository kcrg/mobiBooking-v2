using System.Collections.Generic;
using System.Threading.Tasks;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;

namespace mobiBooking.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByEmailAndPassword(string email, string password);
        Task<bool> UserExist(string email, string userName);
        Task<bool> UserExist(string email, string userName, int id);
        Task<User> FindByEmail(string email);
        Task<IEnumerable<User>> FindRange(IEnumerable<int> Ids);
        Task<IEnumerable<User>> FindAllWithout(int loggedUserid);
    }
}
