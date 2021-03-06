﻿using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.Room.Response;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
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

            bool test1 = await _roomRepository.CheckIfRoomExistsAsync(value.RoomName);

            bool test2 = await _roomRepository.CheckIfAvailabilityExistsAsync(value.AvailabilityId);

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

            if (room == null || room.Reservations.Count > 0)
            {
                return false;
            }


            await _roomRepository.DeleteAsync(room);
            await _roomRepository.Save();
            return true;
        }

        public async Task<RoomDataModel> GetAsync(int id)
        {
            return await _roomRepository.FindRoomAsync(id);
        }

        public async Task<IEnumerable<RoomDataModel>> GetAllAsync(bool orderByName)
        {
            return await _roomRepository.FindAllAsync(orderByName);
        }

        public async Task<IEnumerable<RoomDataModelForReservation>> GetForReservationAsync(RoomsForReservationModel roomForReservationModel)
        {
            if (roomForReservationModel.DateFrom > roomForReservationModel.DateTo)
                return null;
            else
                return await _roomRepository.GetRoomsForReservationAsync(roomForReservationModel);
        }

        public Task<IEnumerable<RoomAvailability>> GetRoomAvailabilitiesAsync()
        {
            return _roomRepository.GetRoomAvailabilitiesAsync();
        }

        public async Task<bool> UpdateAsync(int id, RoomModel value)
        {

            bool a = await _roomRepository.FindAsync(id) == null;
            bool b = await _roomRepository.CheckIfRoomExistsAsync(value.RoomName, id);
            bool c = await _roomRepository.CheckIfAvailabilityExistsAsync(value.AvailabilityId);

            if (a || b || !c)
            {
                return false;
            }

            await _roomRepository.UpdateAsync(id, value);
            await _roomRepository.Save();

            return true;
        }

        public async Task<int> GetReservatedNumberAsync()
        {
            return await _roomRepository.GetReservatedNumberAsync();
        }

        public async Task<int> GetNotReservatedNumberAsync()
        {
            return await _roomRepository.GetNotReservatedNumberAsync();
        }
    }
}
