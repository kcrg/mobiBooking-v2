using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IUsersService
    {
        UserDataModel Authenticate(string email, string password);
        IEnumerable<UserDataModel> GetAll();
    }
}
