using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Component.Enums;
using mobiBooking.Data.Model;
using System;

namespace mobiBooking.Data
{
    public class MobiBookingDBContext : DbContext
    {
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToReservation> UsersToReservations { get; set; }
        public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }
        public virtual DbSet<ReservationInterval> ReservationIntervals { get; set; }

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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Meetings)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.OwnReservations)
                .WithOne(u => u.OwnerUser)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Room>()
               .HasOne(u => u.Availability)
               .WithMany(u => u.Rooms)
               .OnDelete(DeleteBehavior.Restrict);

            CreateTemplateData(modelBuilder);
        }

        private void CreateTemplateData(ModelBuilder modelBuilder)
        {
            byte[] salt = Helpers.GenerateSalt();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "m.w@g.pl",
                Name = "Michał",
                Password = Helpers.HashPassword("123", salt),
                Role = "Administrator",
                Salt = salt,
                Surname = "Test",
                UserName = "Test",
                Active = true
            });

            modelBuilder.Entity<RoomAvailability>().HasData(
                new RoomAvailability
                {
                    Id = 1,
                    Name = "7:00 - 20:00",
                    HoursFrom = 7,
                    HoursTo = 20
                },
                new RoomAvailability
                {
                    Id = 2,
                    Name = "7:00 - 18:00",
                    HoursFrom = 7,
                    HoursTo = 18
                },
                new RoomAvailability
                {
                    Id = 3,
                    Name = "8:00 - 16:00",
                    HoursFrom = 8,
                    HoursTo = 16
                },
                new RoomAvailability
                {
                    Id = 4,
                    Name = "Niedostępna",
                    HoursFrom = 0,
                    HoursTo = 0
                }
            );

            modelBuilder.Entity<ReservationInterval>().HasData(

                new ReservationInterval
                {
                    Id = 1,
                    Name = "Codziennie",
                    Time = Intervals.Day
                },
                new ReservationInterval
                {
                    Id = 2,
                    Name = "Co tydzień",
                    Time = Intervals.Week
                },
                new ReservationInterval
                {
                    Id = 3,
                    Name = "Co 2 tygodnie",
                    Time = Intervals.TwoWeeks
                },
                new ReservationInterval
                {
                    Id = 4,
                    Name = "Co miesiąc",
                    Time = Intervals.Month
                },
                new ReservationInterval
                {
                    Id = 5,
                    Name = "Co rok",
                    Time = Intervals.Year
                }
            );
        }

    }
}
