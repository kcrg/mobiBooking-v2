using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDataModel> GetAll();
        bool Create(CreateUserModel value);
        void Update(int id, CreateUserModel value);
        void Delete(int id);
    }
}
