using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
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

        public Task<bool> CheckIfCanReservAsync(DateTime dateFrom, DateTime dateTo, Room room)
        {
            return (from rooms in DBContext.Rooms
                    where !rooms.Reservations.Where(reserv => Helpers.CheckDateOverlaps(reserv.DateFrom, reserv.DateTo, dateFrom, dateTo)).Any()
                    select rooms).ContainsAsync(room);
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
                CyclicReservation = (bool)value.CyclicReservation,
                ReservationIntervalId = value.ReservationIntervalId
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
