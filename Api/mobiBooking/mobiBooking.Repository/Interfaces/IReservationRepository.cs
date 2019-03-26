using mobiBooking.Data.Model;
using mobiBooking.Model.Meetings.Response;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Response;
using mobiBooking.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Interfaces
{
    public interface IReservationRepository : IRepositoryBase
    {
        Task<bool> CheckIfCanReservAsync(DateTime dateFrom, DateTime dateTo, Room room);
        Task<IEnumerable<ReservationIntervalModel>> GetReservationIntervalsAsync();
        Task CreateAsync(ReservationModel value, Room room, User ownerUser, IEnumerable<User> InvitedUsers);
        Task<IEnumerable<MeetingModel>> GetMeetingsToday(int userId);
        Task<IEnumerable<LastReservationModel>> GetLastReservations(int userId);
    }
}
