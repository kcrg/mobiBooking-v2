using mobiBooking.Data;
using mobiBooking.Data.Model.Users;
using mobiBooking.Repository.Base;
using mobiBooking.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Repository.Repositories
{
    public class UserTypeRepository : RepositoryBase<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(MobiBookingDBContext DBContext) : base(DBContext)
        {
        }
    }
}
