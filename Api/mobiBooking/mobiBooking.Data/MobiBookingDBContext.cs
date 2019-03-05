using Microsoft.EntityFrameworkCore;
using mobiBooking.Data.Model;


namespace mobiBooking.Data
{
    public class MobiBookingDBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersToReservations> UsersToReservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mobiBooking;Trusted_Connection=True;");
        }
    }
}
