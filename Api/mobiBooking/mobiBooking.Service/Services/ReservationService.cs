using mobiBooking.Component.Enums;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Request;
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
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IUserRepository userRepository, IReservationRepository reservationRepository, IUserToReservationRepository userToReservationRepository, IRoomRepository roomRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _userToReservationRepository = userToReservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<bool> Create(ReservationModel value, int OwnerUserId)
        {
            IEnumerable<User> InvitedUsers = await _userRepository.FindRange(value.InvitedUsersIds);
            Room room = await _roomRepository.Find(value.RoomId);
            User OwnerUser = await _userRepository.Find(OwnerUserId);

            if (value.DateFrom == null
                || value.DateTo == null
                || string.IsNullOrEmpty(value.Title)
                || (value.Status != ReservationStatus.Reserved && value.Status != ReservationStatus.NotReserved)
                || !InvitedUsers.Any()
                || InvitedUsers.Count() != value.InvitedUsersIds.Count()
                || room == null
                || !await _reservationRepository.CheckIfCanReserv(value.DateFrom, value.DateTo, room)
                )
            {
                return false;
            }

            Reservation reservation = new Reservation
            {
                DateFrom = value.DateFrom,
                DateTo = value.DateTo,
                OwnerUser = OwnerUser,
                Room = room,
                Status = value.Status,
                Title = value.Title,
                CyclicReservation = (bool)value.CyclicReservation,
                ReservationIntervalId = value.ReservationIntervalId
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

        public async Task<IEnumerable<ReservationIntervalModel>> GetReservationIntervals()
        {
            List<ReservationIntervalModel> reservationIntervals = new List<ReservationIntervalModel>();


            (await _reservationRepository.GetReservationIntervals()).ToList().ForEach(reservationInterval =>
            {
                reservationIntervals.Add(new ReservationIntervalModel
                {
                    Id = reservationInterval.Id,
                    Name = reservationInterval.Name
                });
            });

            return reservationIntervals;
        }

        public Task Update(int id, ReservationModel value)
        {
            throw new NotImplementedException();
        }
    }
}
