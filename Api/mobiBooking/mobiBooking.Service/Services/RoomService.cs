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

        public async Task<bool> Create(RoomModel value)
        {

            if (string.IsNullOrEmpty(value.Availability)
                || string.IsNullOrEmpty(value.Location)
                || string.IsNullOrEmpty(value.RoomName)
                || value.Activity == null
                || value.NumberOfPeople == null
                || value.SoundSystem == null
                || value.Flipchart == null)
            {
                return false;
            }

            await _roomRepository.Create(new Room
            {
                Activity = (bool)value.Activity,
                Availability = value.Availability,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = (int)value.NumberOfPeople,
                SoundSystem = (bool)value.SoundSystem,
                Flipchart = (bool)value.Flipchart
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
                RoomName = room.Name,
                Flipchart = room.Flipchart,
                SoundSystem = room.SoundSystem
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
                    Name = room.Name,
                    Activity = room.Activity,
                    Availability = room.Availability,
                    Location = room.Location,
                    NumberOfPeople = room.NumberOfPeople,
                    SoundSystem = room.SoundSystem,
                    Flipchart = room.Flipchart
                });
            });

            return roomModels;
        }

        public async Task<IEnumerable<RoomDataModel>> GetForReservation(RoomsForReservationModel roomForReservationModel)
        {
            List<RoomDataModel> roomModels = new List<RoomDataModel>();

            (await _roomRepository.GetRoomsForReservation(
                roomForReservationModel.Size,
                roomForReservationModel.DateFrom,
                roomForReservationModel.DateTo,
                roomForReservationModel.SoundSystem,
                roomForReservationModel.FlipChart)).ToList().ForEach(room =>
            {
                roomModels.Add(new RoomDataModel
                {
                    Id = room.Id,
                    Name = room.Name,
                    Activity = room.Activity,
                    Availability = room.Availability,
                    Location = room.Location,
                    NumberOfPeople = room.NumberOfPeople,
                    SoundSystem = room.SoundSystem,
                    Flipchart = room.Flipchart
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
            room.Flipchart = value.Flipchart ?? room.Flipchart;
            room.SoundSystem = value.SoundSystem ?? room.SoundSystem;

            await _roomRepository.Update(room);
            await _roomRepository.Save();
        }
    }
}
