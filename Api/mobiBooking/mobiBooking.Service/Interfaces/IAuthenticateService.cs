using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IAuthenticateService
    {
        TokenModel Authenticate(string email, string password);
        void Logout(int id);
        bool IsLoggedIn(int id);
        string HashPassword(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
