using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace mobiBooking.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IAuthenticateService _authenticateService;

        public UsersService(IRepositoryWrapper repositoryWrapper, IAuthenticateService authenticateService)
        {
            _repositoryWrapper = repositoryWrapper;
            _authenticateService = authenticateService;
        }

        public bool Create(CreateUserModel value)
        {

            if (string.IsNullOrEmpty(value.Email)
                || string.IsNullOrEmpty(value.Name)
                || string.IsNullOrEmpty(value.Password)
                || string.IsNullOrEmpty(value.Surname)
                || string.IsNullOrEmpty(value.UserName)
                || string.IsNullOrEmpty(value.UserType))
            {
                return false;
            }

            if (value.UserType != "Administrator" && value.UserType != "User")
            {
                return false;
            }

            if (_repositoryWrapper.User.UserExist(value.Email, value.UserName))
            {
                return false;
            }

            byte[] salt = _authenticateService.GenerateSalt();

            _repositoryWrapper.User.Create(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = _authenticateService.HashPassword(value.Password, salt),
                Salt = salt,
                Surname = value.Surname,
                UserName = value.UserName,
                Role = value.UserType
            });

            _repositoryWrapper.User.Save();

            return true;
        }

        public void Delete(int id)
        {
            _repositoryWrapper.User.Delete(_repositoryWrapper.User.Find(id));
            _repositoryWrapper.User.Save();
        }

        public IEnumerable<UserDataModel> GetAll()
        {
            List<UserDataModel> users = new List<UserDataModel>();
            _repositoryWrapper.User.FindAll().ToList().ForEach(user =>
            {
                users.Add(new UserDataModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName
                });
            });
            return users;
        }

        public void Update(int id, CreateUserModel value)
        {
            User user = _repositoryWrapper.User.Find(id);

            user.Password = (value.Password != null)
                ? (_authenticateService.HashPassword(value.Password, _authenticateService.GenerateSalt()))
                : (user.Password);
            user.Name = value.Name ?? user.Name;
            user.Surname = value.Surname ?? user.Surname;
            user.UserName = value.UserName ?? user.UserName;
            user.Email = value.Email ?? user.Email;

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();
        }
    }
}
