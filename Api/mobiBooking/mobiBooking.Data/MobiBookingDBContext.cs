using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data.Model;

namespace mobiBooking.Data
{
    public class MobiBookingDBContext : DbContext
    {
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToReservation> UsersToReservations { get; set; }

        public MobiBookingDBContext(DbContextOptions<MobiBookingDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToReservation>()
       .HasKey(bc => new { bc.UserId, bc.ReservationId });
            modelBuilder.Entity<UserToReservation>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Meetings)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserToReservation>()
                .HasOne(bc => bc.Reservation)
                .WithMany(c => c.InvitedUsers)
                .HasForeignKey(bc => bc.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
            .HasMany(c => c.Reservations)
            .WithOne(e => e.Room);

            var salt = Helpers.GenerateSalt();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "m.w@g.pl",
                Name = "Michał",
                Password = Helpers.HashPassword("123", salt),
                Role = "Administrator",
                Salt = salt,
                Surname = "Test",
                UserName = "Test"
            });
        }
    }
}
