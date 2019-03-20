using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<bool> CreateAsync(RoomModel value)
        {

            var test1 = await _roomRepository.CheckIfRoomExistsAsync(value.RoomName);

            var test2 = await _roomRepository.CheckIfAvailabilityExistsAsync(value.Availability);

            if (test1 || !(test2))
            {
                return false;
            }

            await _roomRepository.CreateAsync(value);
            await _roomRepository.Save();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Room room = await _roomRepository.FindAsync(id);

            if (room == null)
            {
                return false;
            }

            await _roomRepository.DeleteAsync(room);
            return true;
        }

        public async Task<RoomModel> GetAsync(int id)
        {
            return await _roomRepository.FindRoomAsync(id);
        }

        public async Task<IEnumerable<RoomDataModel>> GetAllAsync()
        {
            return await _roomRepository.FindAllAsync();
        }

        public async Task<IEnumerable<RoomDataModel>> GetForReservationAsync(RoomsForReservationModel roomForReservationModel)
        {
            return await _roomRepository.GetRoomsForReservationAsync(roomForReservationModel);
        }

        public Task<IEnumerable<RoomAvailability>> GetRoomAvailabilitiesAsync()
        {
            return _roomRepository.GetRoomAvailabilitiesAsync();
        }

        public async Task<bool> UpdateAsync(int id, RoomModel value)
        {

            if (await _roomRepository.FindAsync(id) == null || await _roomRepository.CheckIfRoomExistsAsync(value.RoomName, id))
                return false;

            await _roomRepository.UpdateAsync(id, value);
            await _roomRepository.Save();

            return true;
        }
    }
}
