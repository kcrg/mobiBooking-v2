using mobiBooking.Model.RecivedModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationModel>> GetAll();
        Task<ReservationModel> Get(int id);
        Task<bool> Create(ReservationModel value, int OwnerUserId);
        Task Update(int id, ReservationModel value);
        Task<bool> Delete(int id);
    }
}
