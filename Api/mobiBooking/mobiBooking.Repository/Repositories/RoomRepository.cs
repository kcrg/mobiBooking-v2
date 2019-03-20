using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Repositories
{
    public class RoomRepository : RepositoryBase, IRoomRepository
    {
        public RoomRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }

        public Task<bool> CheckIfRoomExistsAsync(string roomName)
        {
            return (from rooms in DBContext.Rooms
                    where rooms.Name == roomName
                    select rooms).AnyAsync();
        }

        public Task<bool> CheckIfRoomExistsAsync(string roomName, int id)
        {
            return (from rooms in DBContext.Rooms
                    where rooms.Name == roomName
                    where rooms.Id != id
                    select rooms).AnyAsync();
        }

        public Task<IEnumerable<RoomAvailability>> GetRoomAvailabilitiesAsync()
        {
            return Task.Run(() => DBContext.RoomAvailabilities.AsEnumerable());
        }

        public async Task<Room> FindAsync(int roomId)
        {
            return await DBContext.Rooms.Where(r => r.Id == roomId).FirstOrDefaultAsync();
        }

        public Task DeleteAsync(Room room)
        {
            return Task.Run(() => DBContext.Rooms.Remove(room));
        }

        public Task CreateAsync(RoomModel value)
        {
            return Task.Run(() => DBContext.Rooms.AddAsync(new Room
            {
                Activity = (bool)value.Activity,
                AvailabilityId = (int)value.Availability,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = (int)value.NumberOfPeople,
                SoundSystem = (bool)value.SoundSystem,
                Flipchart = (bool)value.Flipchart
            }));
        }

        public async Task<IEnumerable<RoomDataModel>> GetRoomsForReservationAsync(RoomsForReservationModel roomForReservationModel)
        {
            return await (from rooms in DBContext.Rooms
                          where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, roomForReservationModel.DateFrom, roomForReservationModel.DateTo)).Any()
                          where rooms.NumberOfPeople >= roomForReservationModel.Size
                          where (roomForReservationModel.FlipChart ? rooms.Flipchart : true)
                          where (roomForReservationModel.SoundSystem ? rooms.SoundSystem : true)
                          select new RoomDataModel
                          {
                              Activity = rooms.Activity,
                              SoundSystem = rooms.SoundSystem,
                              Availability = rooms.AvailabilityId,
                              Flipchart = rooms.Flipchart,
                              Location = rooms.Location,
                              Name = rooms.Name,
                              NumberOfPeople = rooms.NumberOfPeople,
                              Id = rooms.Id
                          }).ToListAsync();
        }

        public async Task<IEnumerable<RoomDataModel>> FindAllAsync()
        {
            return await DBContext.Rooms.Select(rooms => new RoomDataModel
            {
                Activity = rooms.Activity,
                SoundSystem = rooms.SoundSystem,
                Availability = rooms.AvailabilityId,
                Flipchart = rooms.Flipchart,
                Location = rooms.Location,
                Name = rooms.Name,
                NumberOfPeople = rooms.NumberOfPeople,
                Id = rooms.Id
            }).ToListAsync();
        }

        public async Task<RoomModel> FindRoomAsync(int id)
        {
            return await DBContext.Rooms.Where(r => r.Id == id).Select(rooms => new RoomModel
            {
                Activity = rooms.Activity,
                SoundSystem = rooms.SoundSystem,
                Availability = rooms.AvailabilityId,
                Flipchart = rooms.Flipchart,
                Location = rooms.Location,
                RoomName = rooms.Name,
                NumberOfPeople = rooms.NumberOfPeople
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, RoomModel roomModel)
        {
            Room room = await DBContext.Rooms.FindAsync(id);
            room.Activity = roomModel.Activity;
            room.AvailabilityId = roomModel.Availability;
            room.Flipchart = roomModel.Flipchart;
            room.Location = roomModel.Location;
            room.Name = roomModel.RoomName;
            room.NumberOfPeople = roomModel.NumberOfPeople;
            room.SoundSystem = roomModel.SoundSystem;
            await Task.Run(() => DBContext.Rooms.Update(room));
        }

        public async Task<bool> CheckIfAvailabilityExistsAsync(int availability)
        {
            return await DBContext.RoomAvailabilities.Where(a => a.Id == availability).AnyAsync();
        }
    }
}
