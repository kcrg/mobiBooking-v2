using Microsoft.EntityFrameworkCore;
using mobiBooking.Data.Model;


namespace mobiBooking.Data
{
    public class MobiBookingDBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mobiBooking;Trusted_Connection=True;");
        }
    }
}
