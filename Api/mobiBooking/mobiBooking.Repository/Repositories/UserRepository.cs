using Microsoft.EntityFrameworkCore;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {

        }

        public async Task CreateAsync(CreateUserModel value, string pass, byte[] salt)
        {
            await DBContext.Users.AddAsync(new User
            {
                Email = value.Email,
                Name = value.Name,
                Password = pass,
                Salt = salt,
                Surname = value.Surname,
                UserName = value.UserName,
                Role = value.UserType,
                Active = true
            });
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => DBContext.Remove(DBContext.Users.Where(i => i.Id == id)));
        }

        public async Task<User> FindAsync(int id)
        {
            return await DBContext.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<UserDataModel>> FindActiveUsersAsync()
        {
            return await DBContext.Users
                 .Where(w => !w.Active)
                 .Select(s => new UserDataModel
                 {
                     Active = s.Active,
                     Email = s.Email,
                     Id = s.Id,
                     Name = s.Name,
                     Role = s.Role,
                     Surname = s.Surname,
                     UserName = s.UserName
                 }).ToListAsync();
        }

        public async Task<IEnumerable<UserDataModel>> FindAllAsync()
        {
            return await DBContext.Users
                 .Select(s => new UserDataModel
                 {
                     Active = s.Active,
                     Email = s.Email,
                     Id = s.Id,
                     Name = s.Name,
                     Role = s.Role,
                     Surname = s.Surname,
                     UserName = s.UserName
                 }).ToListAsync();
        }

        public Task<User> FindActiveByEmailAsync(string email)
        {
            return (from Users in DBContext.Users
                    where Users.Email == email
                    where Users.Active == true
                    select Users)
                    .ToAsyncEnumerable()
                    .FirstOrDefault();

        }

        public Task<IEnumerable<User>> FindRangeAsync(IEnumerable<int> Ids)
        {
            return Task.Run(() => (from Users in DBContext.Users
                                   where Ids.Contains(Users.Id)
                                   select Users).AsEnumerable());

        }

        public Task<bool> UserExistAsync(string email, string userName)
        {
            return (from Users in DBContext.Users
                    where Users.Email == email || Users.UserName == userName
                    select Users).AnyAsync();
        }

        public Task<bool> UserExistAsync(string email, string userName, int id)
        {
            return (from Users in DBContext.Users
                    where Users.Email == email || Users.UserName == userName
                    where Users.Id != id
                    select Users).AnyAsync();
        }

        public async Task UpdateAsync(int id, EditUserModel userModel)
        {
            User user = await DBContext.Users.FindAsync(id);
            user.Active = userModel.Active;
            user.Email = userModel.Email;
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.Role = userModel.UserType;
            user.Surname = userModel.Surname;
            user.UserName = userModel.UserName;
            await Task.Run(() => DBContext.Users.Update(user));
        }
    }
}
