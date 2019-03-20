using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDataModel>> GetAllAsync(bool isAdministrator)
        {
            if (isAdministrator) return await _userRepository.FindAllAsync();
            else return await _userRepository.FindActiveUsersAsync();
        }
    }
}
