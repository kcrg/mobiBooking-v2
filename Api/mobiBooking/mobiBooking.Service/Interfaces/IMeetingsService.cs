using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IMeetingsService
    {
        Task<int> GetMeetingsThisWeek(int userId);
    }
}
