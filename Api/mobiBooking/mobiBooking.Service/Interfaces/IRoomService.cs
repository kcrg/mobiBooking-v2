using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDataModel>> GetAll();
        Task<RoomModel> Get(int id);
        Task<bool> Create(RoomModel value);
        Task Update(int id, RoomModel value);
        Task<bool> Delete(int id);
    }
}
