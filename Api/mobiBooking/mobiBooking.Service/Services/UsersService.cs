using mobiBooking.Data.Model.Users;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
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
            throw new NotImplementedException();
        }

        public UserDataModel Get(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
