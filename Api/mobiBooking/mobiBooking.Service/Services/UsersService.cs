using mobiBooking.Component;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<UserDataModel>> GetAll()
        {
            List<UserDataModel> users = new List<UserDataModel>();
            (await _userRepository.FindAll()).ToList().ForEach(user =>
            {
                users.Add(new UserDataModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                });
            });
            return users;
        }
    }
}
