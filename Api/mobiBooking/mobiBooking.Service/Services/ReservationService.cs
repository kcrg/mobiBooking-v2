using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Response;
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
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IUserRepository userRepository, IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<bool> CreateAsync(ReservationModel value, int OwnerUserId)
        {
            IEnumerable<User> InvitedUsers = await _userRepository.FindRangeAsync(value.InvitedUsersIds);
            Room room = await _roomRepository.FindAsync(value.RoomId);
            User OwnerUser = await _userRepository.FindAsync(OwnerUserId);

            if (!InvitedUsers.Any()
                || InvitedUsers.Count() != value.InvitedUsersIds.Count()
                || room == null
                || !await _reservationRepository.CheckIfCanReservAsync(value.DateFrom, value.DateTo, room)
                || value.DateFrom > value.DateTo)
            {
                return false;
            }

            await _reservationRepository.CreateAsync(value, room, OwnerUser, InvitedUsers);

            return true;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReservationIntervalModel>> GetReservationIntervalsAsync()
        {
            return await _reservationRepository.GetReservationIntervalsAsync();
        }

        public Task UpdateAsync(int id, ReservationModel value)
        {
            throw new NotImplementedException();
        }
    }
}
