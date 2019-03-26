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
        Task<TokenModel> AuthenticateAsync(string email, string password);
        Task<bool> CreateAsync(CreateUserModel value);
        Task<bool> UpdateAsync(int id, EditUserModel value);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAstivityAsync(int id, bool activity);
    }
}
