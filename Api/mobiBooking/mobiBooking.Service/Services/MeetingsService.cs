using mobiBooking.Component;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly IUserRepository _userRepository;
        public MeetingsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> GetMeetingsThisWeek(int userId)
        {
             IEnumerable<Reservation> meetings = await _userRepository.GetMeetingsBetweenDatesAsync(DateTime.Now.StartOfWeek(DayOfWeek.Monday), DateTime.Now.EndOfWeek(DayOfWeek.Monday), userId);



            return 2;

        }
    }
}
