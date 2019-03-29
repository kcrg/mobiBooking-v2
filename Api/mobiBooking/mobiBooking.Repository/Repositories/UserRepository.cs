using Microsoft.EntityFrameworkCore;
using mobiBooking.Component;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
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

        public async Task DeleteAsync(int id)
        {
            await Task.Run(async () => DBContext.Users.Remove(await DBContext.Users.Where(i => i.Id == id).FirstOrDefaultAsync()));
        }

        public async Task<User> FindAsync(int id)
        {
            return await DBContext.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDataModel>> FindActiveUsersAsync()
        {
            return await DBContext.Users
                 .Where(w => w.Active)
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
            byte[] salt = Helpers.GenerateSalt();
            User user = await DBContext.Users.FindAsync(id);
            user.Email = userModel.Email;
            user.Name = userModel.Name;
            user.Password = Helpers.HashPassword(userModel.Password, salt);
            user.Salt = salt;
            user.Role = userModel.UserType;
            user.Surname = userModel.Surname;
            user.UserName = userModel.UserName;
            await Task.Run(() => DBContext.Users.Update(user));
        }

        public async Task UpdateAstivityAsync(int id, bool activity)
        {
            byte[] salt = Helpers.GenerateSalt();
            User user = await DBContext.Users.FindAsync(id);
            user.Active = activity;
            await Task.Run(() => DBContext.Users.Update(user));
        }

        public async Task<IEnumerable<Reservation>> GetMeetingsBetweenDatesAsync(DateTime dateFrom, DateTime dateTo, int userId)
        {
            IEnumerable<UserToReservation> userToReservationsList = (from UsersToReservations in DBContext.UsersToReservations.Include(u => u.Reservation)
                                                where UsersToReservations.UserId == userId
                                                select UsersToReservations).AsEnumerable();

            if (!userToReservationsList.Any())
            {
                return new List<Reservation>();
            }

            return await Task.Run(() => userToReservationsList.Where(r => Helpers.CheckDateInside(dateFrom, dateTo, r.Reservation.DateFrom, r.Reservation.DateTo)).Select(r => r.Reservation).AsEnumerable());
        }

        public async Task<UserDataModel> FindUserAsync(int id)
        {
            return await DBContext.Users.Where(u => u.Id == id).Select(u => new UserDataModel
            {
                Active = u.Active,
                Email = u.Email,
                Id = u.Id,
                Name = u.Name,
                Role = u.Role,
                Surname = u.Surname,
                UserName = u.UserName
            }).FirstOrDefaultAsync();
        }

        public async Task<int> GetMeetingsCountBetweenDatesAsync(DateTime dateFrom, DateTime dateTo, int userId)
        {
            return await (from reservation in DBContext.Reservations
                          where reservation.OwnerUserId == userId
                          where Helpers.CheckDateInside(dateFrom, dateTo, reservation.DateFrom, reservation.DateTo)
                          select reservation).CountAsync();         
        }
    }
}
