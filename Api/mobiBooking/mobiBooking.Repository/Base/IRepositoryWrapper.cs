using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Base
{
    public interface IRepositoryWrapper
    {
        IReservationRepository Reservation { get; }
        IRoomRepository Room { get; }
        IUserRepository User { get; }
        IUserToReservationRepository UserToReservation { get; }
    }
}
