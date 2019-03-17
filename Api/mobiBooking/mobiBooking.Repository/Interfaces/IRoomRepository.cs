using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;

namespace mobiBooking.Repository.Interfaces
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<IEnumerable<Room>> FindBySize(int size);
        new Task<IEnumerable<Room>> FindAll();
        Task<IEnumerable<Room>> GetRoomsForReservation(int size, DateTime dateFrom, DateTime dateTo, bool soundSystem, bool flipChart);
        Task<bool> CheckIfCanReserv(DateTime dateFrom, DateTime dateTo, Room room);
    }
}
