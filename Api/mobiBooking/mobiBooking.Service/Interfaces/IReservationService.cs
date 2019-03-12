using mobiBooking.Model.RecivedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Service.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<ReservationModel> GetAll();
        ReservationModel Get(int id);
        bool Create(ReservationModel value, int OwnerUserId);
        void Update(int id, ReservationModel value);
        bool Delete(int id);
    }
}
