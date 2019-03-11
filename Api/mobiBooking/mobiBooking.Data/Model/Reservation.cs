using System;
using System.Collections.Generic;

namespace mobiBooking.Data.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int OwnerUserId { get; set; }
        public int RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public virtual Room Room { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ICollection<UserToReservation> InvitedUsers { get; set; }
    }
}
