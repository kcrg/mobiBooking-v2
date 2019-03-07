using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Data.Model.Reservation;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Repositories
{
    public class UsersToReservationsRepository : RepositoryBase<UsersToReservations>, IUsersToReservationsRepository
    {
        public UsersToReservationsRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
