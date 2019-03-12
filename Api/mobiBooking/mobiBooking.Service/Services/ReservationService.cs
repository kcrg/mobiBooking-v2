using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mobiBooking.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ReservationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public bool Create(ReservationModel value, int OwnerUserId)
        {
            IEnumerable<User> InvitedUsers = _repositoryWrapper.User.FindRange(value.InvitedUsersIds);

            if (value.DateFrom == null
                || value.DateTo == null
                || string.IsNullOrEmpty(value.Status)
                || string.IsNullOrEmpty(value.Title)
                || value.RoomId == 0
                || value.InvitedUsersIds == null)
            {
                return false;
            }

            if (value.InvitedUsersIds.Contains(OwnerUserId))
            {
                return false;
            }

            Reservation reservation = new Reservation
            {
                DateFrom = value.DateFrom,
                DateTo = value.DateTo,
                OwnerUserId = OwnerUserId,
                RoomId = value.RoomId,
                Status = value.Status,
                Title = value.Title
            };

            _repositoryWrapper.Reservation.Create(reservation);

            InvitedUsers.ToList().ForEach(user =>
            {
                _repositoryWrapper.UserToReservation.Create(new UserToReservation
                {
                    Reservation = reservation,
                    User = user
                });
            });

            _repositoryWrapper.Reservation.Save();

            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ReservationModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, ReservationModel value)
        {
            throw new NotImplementedException();
        }
    }
}
