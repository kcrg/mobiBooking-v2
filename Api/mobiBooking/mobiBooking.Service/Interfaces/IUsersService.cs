using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDataModel>> GetAll();
    }
}
