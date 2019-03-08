using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Data.Model.Rooms;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
