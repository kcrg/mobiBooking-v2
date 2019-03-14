using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IAccountService
    {
        Task<TokenModel> Authenticate(string email, string password);
        Task<bool> Create(CreateUserModel value);
        Task Update(int id, CreateUserModel value);
        Task Delete(int id);
    }
}
