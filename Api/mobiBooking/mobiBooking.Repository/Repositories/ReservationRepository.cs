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
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }

        public Task<bool> CheckIfCanReserv(DateTime dateFrom, DateTime dateTo, Room room)
        {
            return (from rooms in DBContext.Rooms
                    where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, dateFrom, dateTo)).Any()
                    select rooms).ContainsAsync(room);
        }

        public Task<IEnumerable<ReservationInterval>> GetReservationIntervals()
        {
            return Task.Run(() => DBContext.ReservationIntervals.AsEnumerable());
        }
    }
}
