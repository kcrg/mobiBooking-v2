using Microsoft.EntityFrameworkCore;
using mobiBooking.Data.Model;
using System;
using System.Security.Cryptography;

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

            //var salt = GenerateSalt();

            //modelBuilder.Entity<User>().HasData(new User {
            //    Email = "m.w@g.pl",
            //    Name = "Michał",
            //    Password = HashPassword("123", salt),
            //    Role = "Administrator",
            //    Salt = salt, Surname = "Test",
            //    UserName = "Test"
            //});
        }
        //private string HashPassword(string password, byte[] salt)
        //{
        //    return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA1,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));
        //}

        //private byte[] GenerateSalt()
        //{
        //    byte[] salt = new byte[128 / 8];
        //    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(salt);
        //    }
        //    return salt;
        //}
    }
}
