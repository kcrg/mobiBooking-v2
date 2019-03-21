using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Component;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
using mobiBooking.Repository.Interfaces;
using mobiBooking.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public AccountService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<TokenModel> AuthenticateAsync(string email, string password)
        {

            User user = await _userRepository.FindActiveByEmailAsync(email);

            if (user == null || user.Password != Helpers.HashPassword(password, user.Salt))
            {
                return null;
            }

            return new TokenModel
            {
                Token = GenerateToken(user)
            };
        }

        public async Task<bool> CreateAsync(CreateUserModel value)
        {

            if (await _userRepository.UserExistAsync(value.Email, value.UserName))
            {
                return false;
            }

            byte[] salt = Helpers.GenerateSalt();
            await _userRepository.CreateAsync(value, Helpers.HashPassword(value.Password, salt), salt);
            await _userRepository.Save();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await _userRepository.FindAsync(id) == null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.Save();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, EditUserModel value)
        {
            User user = await _userRepository.FindAsync(id);

            if (user == null || await _userRepository.UserExistAsync(value.Email, value.UserName, user.Id))
            {
                return false;
            }

            await _userRepository.UpdateAsync(id, value);
            await _userRepository.Save();

            return true;
        }

        private string GenerateToken(User user)
        {
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
    }
}
