using mobiBooking.Data.Model.Users;

namespace mobiBooking.Data.Model.Reservation
{
    public class UsersToReservations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }

        public virtual User User { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
