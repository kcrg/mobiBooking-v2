using mobiBooking.Component;
using mobiBooking.Data.Model;
using mobiBooking.Model.Meetings.Response;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;

        public MeetingsService(IUserRepository userRepository, IReservationRepository reservationRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<string> GetMeetingsLastMonth(int userId)
        {
            var firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            return await GetMeetingsBetween(firstDayOfLastMonth, firstDayOfLastMonth.AddMonths(1).AddSeconds(-1), userId);
        }

        public async Task<string> GetMeetingsLastWeek(int userId)
        {
            return await GetMeetingsBetween(DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(-7), DateTime.Now.EndOfWeek(DayOfWeek.Monday).AddDays(-6).AddSeconds(-1), userId);
        }

        public async Task<string> GetMeetingsThisMonth(int userId)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return await GetMeetingsBetween(firstDayOfMonth, firstDayOfMonth.AddMonths(1).AddSeconds(-1), userId);
        }

        public async Task<string> GetMeetingsThisWeek(int userId)
        {
            var firstDayOfLastMonth = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            return await GetMeetingsBetween(firstDayOfLastMonth, DateTime.Now.EndOfWeek(DayOfWeek.Monday).AddDays(1).AddSeconds(-1), userId);
        }

        public async Task<int> GetMeetingsCountLastMonth(int userId)
        {
            var firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            return await GetMeetingsCountBetween(firstDayOfLastMonth, firstDayOfLastMonth.AddMonths(1).AddSeconds(-1), userId);
        }

        public async Task<int> GetMeetingsCountThisMonth(int userId)
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return await GetMeetingsCountBetween(firstDayOfMonth, firstDayOfMonth.AddMonths(1).AddSeconds(-1), userId);
        }

        public async Task<int> GetMeetingsCountLastWeek(int userId)
        {
            return await GetMeetingsCountBetween(DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(-7), DateTime.Now.EndOfWeek(DayOfWeek.Monday).AddDays(-6).AddSeconds(-1), userId);
        }

        public async Task<int> GetMeetingsCountThisWeek(int userId)
        {
            var firstDayOfLastMonth = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            return await GetMeetingsCountBetween(firstDayOfLastMonth, DateTime.Now.EndOfWeek(DayOfWeek.Monday).AddDays(1).AddSeconds(-1), userId);
        }

        public async Task<IEnumerable<MeetingModel>> GetMeetingsToday(int userId)
        {
            return await _reservationRepository.GetMeetingsToday(userId);
        }
        public async Task<IEnumerable<LastReservationModel>> GetLastReservations(int userId)
        {
            return await _reservationRepository.GetLastReservations(userId);
        }

        private async Task<int> GetMeetingsCountBetween(DateTime dateFrom, DateTime dateTo, int userId)
        {
            return await _userRepository.GetMeetingsCountBetweenDatesAsync(dateFrom, dateTo, userId);
        }

        private async Task<string> GetMeetingsBetween(DateTime dateFrom, DateTime dateTo, int userId)
        {
            IEnumerable<Reservation> meetings = await _userRepository.GetMeetingsBetweenDatesAsync(dateFrom, dateTo, userId);
            TimeSpan i = TimeSpan.Zero;
            meetings.ToList().ForEach(reservation =>
            {
                i += (reservation.DateTo - reservation.DateFrom);
            });

            return i.ToString(@"hh\:mm");
        }
    }
}
