﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using mobiBooking.Data.Model.Users;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

            if (_repositoryWrapper.User.UserExist(value.Email, value.UserName))
            {
                return false;
            }

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            _repositoryWrapper.User.Create(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = _authenticateService.HashPassword(value.Password, salt),
                Salt = Encoding.ASCII.GetString(salt),
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
            var users = new List<UserDataModel>();
            _repositoryWrapper.User.FindAll().ToList().ForEach(user =>
            {
                users.Add(new UserDataModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Token = user.Token,
                    UserName = user.UserName
                });
            });
            return users;
        }

        public void Update(int id, CreateUserModel value)
        {
            var user = _repositoryWrapper.User.Find(id);

            user.Password = (value.Password != null )
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
