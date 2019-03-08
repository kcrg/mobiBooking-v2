using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IAuthenticateService
    {
        UserDataModel Authenticate(string email, string password);
        void Logout(int id);
        bool IsLogin(int id);
    }
}
