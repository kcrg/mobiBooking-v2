﻿using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.SendModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDataModel>> GetAllAsync();
        Task<RoomModel> GetAsync(int id);
        Task<bool> CreateAsync(RoomModel value);
        Task<bool> UpdateAsync(int id, RoomModel value);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RoomDataModel>> GetForReservationAsync(RoomsForReservationModel createReservationModel);
        Task<IEnumerable<RoomAvailability>> GetRoomAvailabilitiesAsync();
    }
}
