using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.Room.Response;
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
                AvailabilityId = value.Availability,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = value.NumberOfPeople
            }));
        }

        public async Task<IEnumerable<RoomDataModelForReservation>> GetRoomsForReservationAsync(RoomsForReservationModel roomForReservationModel)
        {
            

            return await (from rooms in DBContext.Rooms
                          where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, roomForReservationModel.DateFrom, roomForReservationModel.DateTo)).Any()
                          where rooms.NumberOfPeople >= roomForReservationModel.Size
                          where rooms.Availability.HoursFrom <= roomForReservationModel.DateFrom.Hour
                          where rooms.Availability.HoursTo >= roomForReservationModel.DateTo.Hour
                          where roomForReservationModel.DateFrom.Date == roomForReservationModel.DateTo.Date
                          select new RoomDataModelForReservation
                          {
                              Name = rooms.Name,
                              Id = rooms.Id
                          }).ToListAsync();
        }

        public async Task<IEnumerable<RoomDataModel>> FindAllAsync()
        {
            return await DBContext.Rooms.Select(rooms => new RoomDataModel
            {
                Availability = rooms.Availability.Name,
                AvailabilityId = rooms.AvailabilityId,
                Location = rooms.Location,
                Name = rooms.Name,
                NumberOfPeople = rooms.NumberOfPeople,
                Id = rooms.Id
            }).ToListAsync();
        }

        public async Task<RoomDataModel> FindRoomAsync(int id)
        {
            return await DBContext.Rooms.Where(r => r.Id == id).Select(rooms => new RoomDataModel
            {
                Id = rooms.Id,
                AvailabilityId = rooms.AvailabilityId,
                Availability = rooms.Availability.Name,
                Location = rooms.Location,
                Name = rooms.Name,
                NumberOfPeople = rooms.NumberOfPeople
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, RoomModel roomModel)
        {
            Room room = await DBContext.Rooms.FindAsync(id);
            room.AvailabilityId = roomModel.Availability;
            room.Location = roomModel.Location;
            room.Name = roomModel.RoomName;
            room.NumberOfPeople = roomModel.NumberOfPeople;
            await Task.Run(() => DBContext.Rooms.Update(room));
        }

        public async Task<bool> CheckIfAvailabilityExistsAsync(int availability)
        {
            return await DBContext.RoomAvailabilities.Where(a => a.Id == availability).AnyAsync();
        }
    }
}
