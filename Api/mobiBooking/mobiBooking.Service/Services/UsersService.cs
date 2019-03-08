using mobiBooking.Data.Model.Users;
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

        public UsersService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void Create(CreateUserModel value)
        {
            _repositoryWrapper.User.Create(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = value.Password,
                Surname = value.Surname,
                UserName = value.UserName,
                UserStatusId = value.UserStatusId,
                UserTypeId = value.UserTypeId
            });

            _repositoryWrapper.User.Save();
        }

        public void Delete(int id)
        {
            _repositoryWrapper.User.Delete(_repositoryWrapper.User.Find(id));
            _repositoryWrapper.User.Save();
        }

        public IEnumerable<UserDataModel> GetAll()
        {
            var users = new List<UserDataModel>();
            _repositoryWrapper.User.FindAll().ToList().ForEach(user =>
            {
                users.Add(new UserDataModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Token = user.Token,
                    UserStatusId = user.UserStatusId,
                    UserName = user.UserName,
                    UserTypeId = user.UserTypeId
                });
            });
            return users;
        }

        public void Update(int id, CreateUserModel value)
        {
            var user = _repositoryWrapper.User.Find(id);

            user.Name = value.Name;
            user.Password = value.Password;
            user.Surname = value.Surname;
            user.UserName = value.UserName;
            user.UserTypeId = value.UserTypeId;
            user.UserStatusId = value.UserStatusId;
            user.Email = value.Email;

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();
        }
    }
}
