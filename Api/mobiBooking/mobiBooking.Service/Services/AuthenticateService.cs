﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Data.Model.Users;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mobiBooking.Service.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AuthenticateService(IOptions<AppSettings> appSettings, IRepositoryWrapper repositoryWrapper)
        {
            _appSettings = appSettings.Value;
            _repositoryWrapper = repositoryWrapper;
        }

        public UserDataModel Authenticate(string email, string password)
        {
            User user = _repositoryWrapper.User.FindByEmailAndPassword(email, password);

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
                UserStatusId = user.UserStatusId,
                UserTypeId = user.UserTypeId
            };

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();

            return userDataModel;
        }

        public bool HasPermission(int id, string permission)
        {
           return _repositoryWrapper.User.CheckPermission(id, permission);
        }

        public void LogOut(int id)
        {
            throw new NotImplementedException();
        }
    }
}