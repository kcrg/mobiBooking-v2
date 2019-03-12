using System;
using System.Collections.Generic;
using System.Text;
using mobiBooking.Data;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Repository.Repositories;

namespace mobiBooking.Repository.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private MobiBookingDBContext _DBContext;

        private IReservationRepository _reservation;
        private IUserRepository _user;
        private IRoomRepository _room;
        private IUserToReservationRepository _userToReservation;

        public RepositoryWrapper(MobiBookingDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public IReservationRepository Reservation
        {
            get
            {
                if (_reservation == null)
                {
                    _reservation = new ReservationRepository(_DBContext);
                }
                return _reservation;
            }
        }

        public IRoomRepository Room
        {
            get
            {
                if (_room == null)
                {
                    _room = new RoomRepository(_DBContext);
                }
                return _room;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_DBContext);
                }
                return _user;
            }
        }

        public IUserToReservationRepository UserToReservation
        {
            get
            {
                if (_userToReservation == null)
                {
                    _userToReservation = new UserToReservationRepository(_DBContext);
                }
                return _userToReservation;
            }
        }
    }
}
