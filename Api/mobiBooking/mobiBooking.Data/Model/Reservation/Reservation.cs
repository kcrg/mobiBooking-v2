using mobiBooking.Data.Model.Rooms;
using mobiBooking.Data.Model.Users;
using System;


namespace mobiBooking.Data.Model.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }       
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }

        public virtual ReservationStatus Status { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
