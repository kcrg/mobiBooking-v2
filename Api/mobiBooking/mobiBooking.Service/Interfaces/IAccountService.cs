using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
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
        Task<bool> Update(int id, EditUserModel value);
        Task Delete(int id);
    }
}
