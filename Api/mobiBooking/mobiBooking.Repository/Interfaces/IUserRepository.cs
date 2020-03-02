using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mobiBooking.Data.Model;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
using mobiBooking.Repository.Base;

namespace mobiBooking.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase
    {
        Task<bool> UserExistAsync(string email, string userName);
        Task<bool> UserExistAsync(string email, string userName, int id);
        Task<User> FindActiveByEmailAsync(string email);
        Task<IEnumerable<User>> FindRangeAsync(IEnumerable<int> Ids);
        Task<IEnumerable<Reservation>> GetMeetingsBetweenDatesAsync(DateTime dateFrom, DateTime dateTo, int userId);
        Task<IEnumerable<UserDataModel>> FindActiveUsersAsync();
        Task<IEnumerable<UserDataModel>> FindAllAsync();
        Task<UserDataModel> FindUserAsync(int id);
        Task CreateAsync(CreateUserModel value, string pass, byte[] salt);
        Task DeleteAsync(int id);
        Task<User> FindAsync(int id);
        Task UpdateAsync(int id, EditUserModel user);
        Task UpdateAstivityAsync(int id, bool activity);
        Task<int> GetMeetingsCountBetweenDatesAsync(DateTime dateFrom, DateTime dateTo, int userId);
    }
}
