using Microsoft.EntityFrameworkCore;
using mobiBooking.Data.Model.Reservation;
using mobiBooking.Data.Model.Rooms;
using mobiBooking.Data.Model.Users;

namespace mobiBooking.Data
{
    public class MobiBookingDBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersToReservations> UsersToReservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mobiBooking;Trusted_Connection=True;");
        }
    }
}
