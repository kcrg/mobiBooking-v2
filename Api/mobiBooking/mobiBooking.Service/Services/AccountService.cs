using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mobiBooking.Component;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
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

        public async Task<TokenModel> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = await _userRepository.FindByEmail(email);

            if (user == null)
            {
                return null;
            }

            string salt = Helpers.HashPassword(password, user.Salt);

            if (user.Password != salt)
            {
                return null;
            }

            string token = GenerateToken(user);

            TokenModel userDataModel = new TokenModel
            {
                Token = token
            };

            await _userRepository.Update(user);
            await _userRepository.Save();

            return userDataModel;
        }

        public async Task<bool> Create(CreateUserModel value)
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

            if (await _userRepository.UserExist(value.Email, value.UserName))
            {
                return false;
            }

            byte[] salt = Helpers.GenerateSalt();

            await _userRepository.Create(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = Helpers.HashPassword(value.Password, salt),
                Salt = salt,
                Surname = value.Surname,
                UserName = value.UserName,
                Role = value.UserType
            });

            await _userRepository.Save();

            return true;
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(await _userRepository.Find(id));
            await _userRepository.Save();
        }

        public async Task Update(int id, CreateUserModel value)
        {
            User user = await _userRepository.Find(id);

            user.Password = (value.Password != null)
                ? (Helpers.HashPassword(value.Password, Helpers.GenerateSalt()))
                : (user.Password);
            user.Name = value.Name ?? user.Name;
            user.Surname = value.Surname ?? user.Surname;
            user.UserName = value.UserName ?? user.UserName;
            user.Email = value.Email ?? user.Email;

            await _userRepository.Update(user);
            await _userRepository.Save();
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
    }
}
