using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Repositories
{
    public class UserToReservationRepository : RepositoryBase<UserToReservation>, IUserToReservationRepository
    {
        public UserToReservationRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
