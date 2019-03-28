using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Component.Enums;
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
                AvailabilityId = value.AvailabilityId,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = value.NumberOfPeople
            }));
        }

        public async Task<IEnumerable<RoomDataModelForReservation>> GetRoomsForReservationAsync(RoomsForReservationModel roomForReservationModel)
        {

            List<Room> roomsList = await (from rooms in DBContext.Rooms.Include(r => r.Reservations).ThenInclude(r => r.ReservationInterval)
                                          where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, roomForReservationModel.DateFrom, roomForReservationModel.DateTo)).Any()
                                          where rooms.NumberOfPeople >= roomForReservationModel.Size
                                          where Helpers.CheckDateInside(
                                              roomForReservationModel.DateFrom.Date.AddHours(rooms.Availability.HoursFrom),
                                              roomForReservationModel.DateTo.Date.AddHours(rooms.Availability.HoursTo),
                                              roomForReservationModel.DateFrom,
                                              roomForReservationModel.DateTo)
                                          where roomForReservationModel.DateFrom.Date == roomForReservationModel.DateTo.Date
                                          select rooms
                          ).ToListAsync();

            return roomsList.Where(rooms => !rooms.Reservations.Where(reserv => Helpers.CheckIntervalReservation(reserv.DateFrom,
                reserv.DateTo,
                roomForReservationModel.DateFrom,
                roomForReservationModel.DateTo,
                reserv.ReservationInterval?.Time,
                reserv.CyclicReservation))
                .Any())
                .Select(r => new RoomDataModelForReservation
                {
                    Id = r.Id,
                    Name = r.Name
                });
        }

        public async Task<int> GetNotReservatedNumberAsync()
        {
            List<Room> roomsList = await (from rooms in DBContext.Rooms.Include(r => r.Reservations).ThenInclude(r => r.ReservationInterval)
                                          where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, DateTime.Now, DateTime.Now.AddMinutes(1))).Any()
                                          where Helpers.CheckDateInside(
                                              DateTime.Now.Date.AddHours(rooms.Availability.HoursFrom),
                                              DateTime.Now.Date.AddHours(rooms.Availability.HoursTo),
                                              DateTime.Now,
                                              DateTime.Now)
                                          select rooms).ToListAsync();

            return roomsList.Where(rooms => !rooms.Reservations.Where(reserv => Helpers.CheckIntervalReservation(reserv.DateFrom,
                reserv.DateTo,
                DateTime.Now,
                DateTime.Now.AddMinutes(1),
                reserv.ReservationInterval?.Time,
                reserv.CyclicReservation))
                .Any())
                .Count();
        }

        public async Task<int> GetReservatedNumberAsync()
        {
            return await (from rooms in DBContext.Rooms.Include(r => r.Reservations).ThenInclude(r => r.ReservationInterval)
                          where rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, DateTime.Now, DateTime.Now.AddMinutes(1))
                                                                  || Helpers.CheckIntervalReservation(reserv.DateFrom,
                                                                                reserv.DateTo,
                                                                                DateTime.Now,
                                                                                DateTime.Now.AddMinutes(1),
                                                                                reserv.ReservationInterval == null ? Intervals.Day : reserv.ReservationInterval.Time,
                                                                                reserv.CyclicReservation)).Any()
                          || !Helpers.CheckDateInside(
                              DateTime.Now.Date.AddHours(rooms.Availability.HoursFrom),
                              DateTime.Now.Date.AddHours(rooms.Availability.HoursTo),
                              DateTime.Now,
                              DateTime.Now.AddMinutes(1))
                          select rooms).CountAsync();
        }

        public async Task<IEnumerable<RoomDataModel>> FindAllAsync(bool orderByName)
        {

            if (orderByName)
            {
                return await DBContext.Rooms.OrderBy(r => r.Name).Select(rooms => new RoomDataModel
                {
                    Availability = rooms.Availability.Name,
                    AvailabilityId = rooms.AvailabilityId,
                    Location = rooms.Location,
                    RoomName = rooms.Name,
                    NumberOfPeople = rooms.NumberOfPeople,
                    Id = rooms.Id
                }).ToListAsync();
            }
            else
            {
                return await DBContext.Rooms.OrderBy(r => r.NumberOfPeople).Select(rooms => new RoomDataModel
                {
                    Availability = rooms.Availability.Name,
                    AvailabilityId = rooms.AvailabilityId,
                    Location = rooms.Location,
                    RoomName = rooms.Name,
                    NumberOfPeople = rooms.NumberOfPeople,
                    Id = rooms.Id
                }).ToListAsync();
            }
        }

        public async Task<RoomDataModel> FindRoomAsync(int id)
        {
            return await DBContext.Rooms.Where(r => r.Id == id).Select(rooms => new RoomDataModel
            {
                Id = rooms.Id,
                AvailabilityId = rooms.AvailabilityId,
                Availability = rooms.Availability.Name,
                Location = rooms.Location,
                RoomName = rooms.Name,
                NumberOfPeople = rooms.NumberOfPeople
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, RoomModel roomModel)
        {
            Room room = await DBContext.Rooms.FindAsync(id);
            room.AvailabilityId = roomModel.AvailabilityId;
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