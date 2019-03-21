using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.Room.Response;
using mobiBooking.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Interfaces
{
    public interface IRoomRepository : IRepositoryBase
    {
        Task<bool> CheckIfRoomExistsAsync(string roomName);
        Task<bool> CheckIfRoomExistsAsync(string roomName, int id);
        Task<IEnumerable<RoomAvailability>> GetRoomAvailabilitiesAsync();
        Task UpdateAsync(int id, RoomModel room);
        Task DeleteAsync(Room room);
        Task CreateAsync(RoomModel value);
        Task<IEnumerable<RoomDataModelForReservation>> GetRoomsForReservationAsync(RoomsForReservationModel roomForReservationModel);
        Task<bool> CheckIfAvailabilityExistsAsync(int availability);
        Task<IEnumerable<RoomDataModel>> FindAllAsync(bool orderByName);
        Task<RoomDataModel> FindRoomAsync(int id);
        Task<Room> FindAsync(int id);
    }
}
