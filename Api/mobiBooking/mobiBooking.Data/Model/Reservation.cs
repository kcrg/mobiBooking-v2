using mobiBooking.Component.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mobiBooking.Data.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int? OwnerUserId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ReservationStatus Status { get; set; }
        [Required]
        public string Title { get; set; }
        public bool CyclicReservation { get; set; }
        public int? ReservationIntervalId { get; set; }

        public virtual Room Room { get; set; }
        public virtual ReservationInterval ReservationInterval { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual ICollection<UserToReservation> InvitedUsers { get; set; }
    }
}
