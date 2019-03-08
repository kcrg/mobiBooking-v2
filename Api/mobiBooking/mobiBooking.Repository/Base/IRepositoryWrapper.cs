﻿using mobiBooking.Repository.Interfaces;
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
        IUsersToReservationsRepository UsersToReservations { get; }
        IReservationStatusRepository ReservationStatus { get; }
        IRoleRepository Role { get; }
        IUserStatusRepository UserStatus { get; }
        IUserTypeRepository UserType { get; }
    }
}
