using mobiBooking.Model.SendModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDataModel>> GetAllAsync(bool isAdministrator);
        Task<UserDataModel> GetAsync(int id);
    }
}
