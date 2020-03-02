using mobiBooking.Model.Meetings.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IMeetingsService
    {
        Task<string> GetMeetingsLastMonth(int userId);
        Task<string> GetMeetingsLastWeek(int userId);
        Task<string> GetMeetingsThisMonth(int userId);
        Task<string> GetMeetingsThisWeek(int userId);
        Task<IEnumerable<MeetingModel>> GetMeetingsToday(int userId);
        Task<IEnumerable<LastReservationModel>> GetLastReservations(int userId);
        Task<int> GetMeetingsCountLastMonth(int userId);
        Task<int> GetMeetingsCountThisMonth(int userId);
        Task<int> GetMeetingsCountLastWeek(int userId);
        Task<int> GetMeetingsCountThisWeek(int userId);
    }
}
