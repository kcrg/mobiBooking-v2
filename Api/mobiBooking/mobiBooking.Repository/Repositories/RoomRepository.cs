using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }

        public Task<IEnumerable<Room>> FindBySize(int size)
        {
            return Task.Run(() => (from Rooms in DBContext.Rooms
                                   join Reservations in DBContext.Reservations
                                   on Rooms.Id equals Reservations.RoomId
                                   where Rooms.NumberOfPeople >= size
                                   select Rooms).AsEnumerable());
        }

        public new Task<IEnumerable<Room>> FindAll()
        {
            return Task.Run<IEnumerable<Room>>(() => DBContext.Rooms.Include(s => s.Reservations));
        }

        public Task<IEnumerable<Room>> GetRoomsForReservation(int size, DateTime dateFrom, DateTime dateTo, bool soundSystem, bool flipChart)
        {

            return Task.Run(() => (from rooms in DBContext.Rooms
                                   where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, dateFrom, dateTo)).Any()
                                   where rooms.NumberOfPeople >= size
                                   where (flipChart ? rooms.Flipchart : true)
                                   where (soundSystem ? rooms.SoundSystem : true)
                                   select rooms).AsEnumerable());
        }

        public Task<bool> CheckIfCanReserv(DateTime dateFrom, DateTime dateTo, Room room)
        {
            return (from rooms in DBContext.Rooms
                    where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, dateFrom, dateTo)).Any()
                    select rooms).ContainsAsync(room);
        }
    }
}
