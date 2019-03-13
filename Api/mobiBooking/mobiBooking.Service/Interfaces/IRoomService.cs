using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDataModel> GetAll();
        RoomModel Get(int id);
        bool Create(RoomModel value);
        void Update(int id, RoomModel value);
        bool Delete(int id);
    }
}
