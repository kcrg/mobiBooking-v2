﻿using mobiBooking.Data;
using mobiBooking.Data.Model.Reservation;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Repositories
{
    public class ReservationStatusRepository : RepositoryBase<ReservationStatus>, IReservationStatusRepository
    {
        public ReservationStatusRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
