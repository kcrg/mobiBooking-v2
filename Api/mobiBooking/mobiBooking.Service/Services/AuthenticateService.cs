using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Data.Model;
using mobiBooking.Model.SendModels;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public TokenModel Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = _repositoryWrapper.User.FindByEmail(email);

            if (user == null)
            {
                return null;
            }

            string salt = HashPassword(password, user.Salt);

            if (user.Password != salt)
            {
                return null;
            }

            user.Token = GenerateToken(user);

            TokenModel userDataModel = new TokenModel
            {
                Token = user.Token
            };

            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();

            return userDataModel;
        }

        private string GenerateToken(User user)
        {
            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("role", user.Role),
                    new Claim("userName", user.UserName),
                    new Claim("name", user.Name),
                    new Claim("sureName", user.Surname),
                    new Claim("email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool IsLoggedIn(int id)
        {
            string token = _repositoryWrapper.User.Find(id).Token;
            return token != null;
        }

        public void Logout(int id)
        {
            User user = _repositoryWrapper.User.Find(id);

            if (user == null)
                return;

            user.Token = null;
            _repositoryWrapper.User.Update(user);
            _repositoryWrapper.User.Save();
        }

        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
