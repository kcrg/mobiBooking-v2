using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mobiBooking.Data.Migrations
{
    public partial class A : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationIntervals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    HoursFrom = table.Column<int>(nullable: false),
                    HoursTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAvailabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    AvailabilityId = table.Column<int>(nullable: false),
                    NumberOfPeople = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomAvailabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "RoomAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(nullable: false),
                    OwnerUserId = table.Column<int>(nullable: true),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    CyclicReservation = table.Column<bool>(nullable: false),
                    ReservationIntervalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationIntervals_ReservationIntervalId",
                        column: x => x.ReservationIntervalId,
                        principalTable: "ReservationIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersToReservations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToReservations", x => new { x.UserId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_UsersToReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersToReservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ReservationIntervals",
                columns: new[] { "Id", "Name", "Time" },
                values: new object[,]
                {
                    { 1, "Codziennie", 0 },
                    { 2, "Co tydzień", 1 },
                    { 3, "Co 2 tygodnie", 2 },
                    { 4, "Co miesiąc", 3 },
                    { 5, "Co rok", 4 }
                });

            migrationBuilder.InsertData(
                table: "RoomAvailabilities",
                columns: new[] { "Id", "HoursFrom", "HoursTo", "Name" },
                values: new object[,]
                {
                    { 1, 7, 20, "7:00 - 20:00" },
                    { 2, 7, 18, "7:00 - 18:00" },
                    { 3, 8, 16, "8:00 - 16:00" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Email", "Name", "Password", "Role", "Salt", "Surname", "UserName" },
                values: new object[] { 1, true, "m.w@g.pl", "Michał", "y25AsgfWaOF9dvk3y7QfFQcMx55w6vTTH32Kiowb4vg=", "Administrator", new byte[] { 92, 154, 159, 209, 44, 229, 176, 40, 8, 94, 18, 187, 132, 204, 105, 251 }, "Test", "Test" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OwnerUserId",
                table: "Reservations",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationIntervalId",
                table: "Reservations",
                column: "ReservationIntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AvailabilityId",
                table: "Rooms",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToReservations_ReservationId",
                table: "UsersToReservations",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersToReservations");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ReservationIntervals");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomAvailabilities");
        }
    }
}
