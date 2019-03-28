﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mobiBooking.Data;

namespace mobiBooking.Data.Migrations
{
    [DbContext(typeof(MobiBookingDBContext))]
    partial class MobiBookingDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("mobiBooking.Data.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CyclicReservation");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int?>("OwnerUserId");

                    b.Property<int?>("ReservationIntervalId");

                    b.Property<int>("RoomId");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("ReservationIntervalId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("mobiBooking.Data.Model.ReservationInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.ToTable("ReservationIntervals");

                    b.HasData(
                        new { Id = 1, Name = "Codziennie", Time = 0 },
                        new { Id = 2, Name = "Co tydzień", Time = 1 },
                        new { Id = 3, Name = "Co 2 tygodnie", Time = 2 },
                        new { Id = 4, Name = "Co miesiąc", Time = 3 },
                        new { Id = 5, Name = "Co rok", Time = 4 }
                    );
                });

            modelBuilder.Entity("mobiBooking.Data.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailabilityId");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfPeople");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("mobiBooking.Data.Model.RoomAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HoursFrom");

                    b.Property<int>("HoursTo");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RoomAvailabilities");

                    b.HasData(
                        new { Id = 1, HoursFrom = 7, HoursTo = 20, Name = "7:00 - 20:00" },
                        new { Id = 2, HoursFrom = 7, HoursTo = 18, Name = "7:00 - 18:00" },
                        new { Id = 3, HoursFrom = 8, HoursTo = 16, Name = "8:00 - 16:00" }
                    );
                });

            modelBuilder.Entity("mobiBooking.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Surname");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, Active = true, Email = "m.w@g.pl", Name = "Michał", Password = "48e02qy/5QG/ihx8q1pTlqTvo6fLM3dYA965qkOegQ8=", Role = "Administrator", Salt = new byte[] { 30, 99, 120, 53, 34, 208, 212, 46, 124, 134, 38, 73, 233, 44, 189, 53 }, Surname = "Test", UserName = "Test" }
                    );
                });

            modelBuilder.Entity("mobiBooking.Data.Model.UserToReservation", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ReservationId");

                    b.HasKey("UserId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("UsersToReservations");
                });

            modelBuilder.Entity("mobiBooking.Data.Model.Reservation", b =>
                {
                    b.HasOne("mobiBooking.Data.Model.User", "OwnerUser")
                        .WithMany("OwnReservations")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("mobiBooking.Data.Model.ReservationInterval", "ReservationInterval")
                        .WithMany()
                        .HasForeignKey("ReservationIntervalId");

                    b.HasOne("mobiBooking.Data.Model.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("mobiBooking.Data.Model.Room", b =>
                {
                    b.HasOne("mobiBooking.Data.Model.RoomAvailability", "Availability")
                        .WithMany("Rooms")
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mobiBooking.Data.Model.UserToReservation", b =>
                {
                    b.HasOne("mobiBooking.Data.Model.Reservation", "Reservation")
                        .WithMany("InvitedUsers")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("mobiBooking.Data.Model.User", "User")
                        .WithMany("Meetings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
