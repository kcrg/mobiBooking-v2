using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using mobiBooking.Data.Model.Users;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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

            // generate a 128-bit salt using a secure PRNG
            //byte[] salt = new byte[128 / 8];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}

            //// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            //string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //    password: value.Password,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA1,
            //    iterationCount: 10000,
            //    numBytesRequested: 256 / 8));

            _repositoryWrapper.User.Create(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = value.Password,
                Surname = value.Surname,
                UserName = value.UserName,
                Role = value.UserType
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
                    UserName = user.UserName
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
            user.Email = value.Email;

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();
        }
    }
}
