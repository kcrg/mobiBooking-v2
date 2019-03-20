using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationModel>> GetAllAsync();
        Task<ReservationModel> GetAsync(int id);
        Task<bool> CreateAsync(ReservationModel value, int OwnerUserId);
        Task UpdateAsync(int id, ReservationModel value);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ReservationIntervalModel>> GetReservationIntervalsAsync();
    }
}
