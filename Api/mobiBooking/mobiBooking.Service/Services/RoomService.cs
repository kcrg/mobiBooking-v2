using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
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

        public async Task<bool> Create(RoomModel value)
        {

            if (string.IsNullOrEmpty(value.Availability)
                || string.IsNullOrEmpty(value.Location)
                || string.IsNullOrEmpty(value.RoomName)
                || value.Activity == null
                || value.NumberOfPeople == null)
            {
                return false;
            }

            await _roomRepository.Create(new Room
            {
                Activity = (bool)value.Activity,
                Availability = value.Availability,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = (int)value.NumberOfPeople
            });
            await _roomRepository.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Room room = await _roomRepository.Find(id);

            if (room == null)
            {
                return false;
            }

            await _roomRepository.Delete(room);
            return true;
        }

        public async Task<RoomModel> Get(int id)
        {
            Room room = await _roomRepository.Find(id);

            if (room == null)
            {
                return null;
            }

            return new RoomModel
            {
                Activity = room.Activity,
                Availability = room.Availability,
                Location = room.Location,
                NumberOfPeople = room.NumberOfPeople,
                RoomName = room.Name
            };
        }

        public async Task<IEnumerable<RoomDataModel>> GetAll()
        {
            List<RoomDataModel> roomModels = new List<RoomDataModel>();

            (await _roomRepository.FindAll()).ToList().ForEach(room =>
            {
                roomModels.Add(new RoomDataModel
                {
                    Id = room.Id,
                    Name = room.Name
                });
            });

            return roomModels;
        }

        public async Task Update(int id, RoomModel value)
        {
            Room room = await _roomRepository.Find(id);

            room.Activity = value.Activity ?? room.Activity;
            room.Availability = value.Availability ?? room.Availability;
            room.Location = value.Location ?? room.Location;
            room.Name = value.RoomName ?? room.Name;
            room.NumberOfPeople = value.NumberOfPeople ?? room.NumberOfPeople;

            await _roomRepository.Update(room);
            await _roomRepository.Save();
        }
    }
}
