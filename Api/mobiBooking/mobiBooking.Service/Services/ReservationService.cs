using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserToReservationRepository _userToReservationRepository;

        public ReservationService(IUserRepository userRepository, IReservationRepository reservationRepository, IUserToReservationRepository userToReservationRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _userToReservationRepository = userToReservationRepository;
        }

        public async Task<bool> Create(ReservationModel value, int OwnerUserId)
        {
            IEnumerable<User> InvitedUsers = await _userRepository.FindRange(value.InvitedUsersIds);

            if (value.DateFrom == null
                || value.DateTo == null
                || string.IsNullOrEmpty(value.Status)
                || string.IsNullOrEmpty(value.Title)
                || value.RoomId == 0
                || value.InvitedUsersIds == null)
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

            await _reservationRepository.Create(reservation);

            InvitedUsers.ToList().ForEach(user =>
            {
                _userToReservationRepository.Create(new UserToReservation
                {
                    Reservation = reservation,
                    User = user
                });
            });

            await _reservationRepository.Save();

            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, ReservationModel value)
        {
            throw new NotImplementedException();
        }
    }
}
