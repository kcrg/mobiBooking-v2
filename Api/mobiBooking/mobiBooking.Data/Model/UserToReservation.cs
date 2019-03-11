using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class UserToReservation
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
