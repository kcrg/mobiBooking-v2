using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace mobiBooking.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UsersService(IOptions<AppSettings> appSettings, IRepositoryWrapper repositoryWrapper)
        {
            _appSettings = appSettings.Value;
            _repositoryWrapper = repositoryWrapper;
        }

        public UserDataModel Authenticate(string email, string password)
        {
            var user = _repositoryWrapper.User.FindByEmailAndPassword(email, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            UserDataModel userDataModel = new UserDataModel
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Token = user.Token,
                UserName = user.UserName,
                UserStatus = user.UserStatus, 
                UserType = user.UserType
            };

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();

            return userDataModel;
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
                    UserStatus = user.UserStatus,
                    UserName = user.UserName,
                    UserType = user.UserType
                });
            });
            return users;
        }
    }
}
