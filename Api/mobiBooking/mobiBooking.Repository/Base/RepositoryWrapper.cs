﻿using System;
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
        private IUsersToReservationsRepository _usersToReservations;
        private IReservationStatusRepository _reservationStatus;
        private IUserTypeRepository _userType;
        private IUserStatusRepository _userStatus;
        private IRoleRepository _role;

        public RepositoryWrapper(MobiBookingDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public IReservationRepository Reservation
        {
            get {
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

        public IUsersToReservationsRepository UsersToReservations
        {
            get
            {
                if (_usersToReservations == null)
                {
                    _usersToReservations = new UsersToReservationsRepository(_DBContext);
                }
                return _usersToReservations;
            }
        }

        public IReservationStatusRepository ReservationStatus
        {
            get
            {
                if (_reservationStatus == null)
                {
                    _reservationStatus = new ReservationStatusRepository(_DBContext);
                }
                return _reservationStatus;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_DBContext);
                }
                return _role;
            }
        }

        public IUserStatusRepository UserStatus
        {
            get
            {
                if (_userStatus == null)
                {
                    _userStatus = new UserStatusRepository(_DBContext);
                }
                return _userStatus;
            }
        }

        public IUserTypeRepository UserType{
            get
            {
                if (_userType == null)
                {
                    _userType = new UserTypeRepository(_DBContext);
                }
                return _userType;
            }
        }
    }
}