using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<bool> CheckIfCanReserv(DateTime dateFrom, DateTime dateTo, Room room);
        Task<IEnumerable<ReservationInterval>> GetReservationIntervals();
    }
}
