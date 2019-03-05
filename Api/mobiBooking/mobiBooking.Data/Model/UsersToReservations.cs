using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class UsersToReservations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
    }
}
