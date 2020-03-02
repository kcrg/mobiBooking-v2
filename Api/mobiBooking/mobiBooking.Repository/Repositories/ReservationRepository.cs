using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Model.Meetings.Response;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Response;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Repositories
{
    public class ReservationRepository : RepositoryBase, IReservationRepository
    {
        public ReservationRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }

        public async Task<bool> CheckIfCanReservAsync(DateTime dateFrom, DateTime dateTo, Room room)
        {
            List<Room> roomsList = await (from rooms in DBContext.Rooms.Include(r => r.Availability).Include(r => r.Reservations).ThenInclude(r => r.ReservationInterval)
                                          where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, dateFrom, dateTo)).Any()
                                          where Helpers.CheckDateInside(
                                                   dateFrom.Date.AddHours(rooms.Availability.HoursFrom),
                                                   dateFrom.Date.AddHours(rooms.Availability.HoursTo),
                                                   dateFrom,
                                                   dateFrom)
                                          select rooms).ToListAsync();

            // if(roomsList.)

            return roomsList.Where(rooms => !rooms.Reservations.Where(reserv => Helpers.CheckIntervalReservation(reserv.DateFrom,
                reserv.DateTo,
                dateFrom,
                dateTo,
                reserv.ReservationInterval?.Time,
                reserv.CyclicReservation)).Any()).Contains(room);

        }

        public async Task CreateAsync(ReservationModel value, Room room, User ownerUser, IEnumerable<User> InvitedUsers)
        {
            Reservation reservation = new Reservation
            {
                DateFrom = value.DateFrom,
                DateTo = value.DateTo,
                OwnerUser = ownerUser,
                Room = room,
                Status = value.Status,
                Title = value.Title,
                CyclicReservation = value.CyclicReservation,
                ReservationIntervalId = value.CyclicReservation ? value.ReservationIntervalId : null
            };

            await DBContext.Reservations.AddAsync(reservation);

            InvitedUsers.ToList().ForEach(async user =>
            {
                await DBContext.UsersToReservations.AddAsync(new UserToReservation
                {
                    ReservationId = reservation.Id,
                    UserId = user.Id
                });
            });

            await DBContext.SaveChangesAsync();
        }

        public Task<IEnumerable<MeetingModel>> GetMeetingsToday(int userId)
        {
            return Task.Run(() => (from reservations in DBContext.Reservations
                                   where reservations.InvitedUsers.Select(u => u.UserId).Contains(userId)
                                   where Helpers.CheckDateInside(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1), reservations.DateFrom, reservations.DateTo)
                                   orderby reservations.DateFrom
                                   select new MeetingModel
                                   {
                                       RoomName = reservations.Room.Name,
                                       Time = reservations.DateFrom.ToString("HH:mm"),
                                       Title = reservations.Title
                                   }).AsEnumerable());
        }

        public Task<IEnumerable<LastReservationModel>> GetLastReservations(int userId)
        {

            return Task.Run(() => (from reservations in DBContext.Reservations
                                   where reservations.OwnerUserId == userId
                                   orderby reservations.DateFrom descending
                                   select new LastReservationModel
                                   {
                                       RoomName = reservations.Room.Name,
                                       Date = reservations.DateFrom.ToString(@"dd.MM.yyyy HH:mm"),//y-M-d h:m:s.fffffff
                                       Title = reservations.Title
                                   }).Take(15).AsEnumerable());
        }

        public async Task<IEnumerable<ReservationIntervalModel>> GetReservationIntervalsAsync()
        {
            return await DBContext.ReservationIntervals.Select(r => new ReservationIntervalModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }
    }
}
