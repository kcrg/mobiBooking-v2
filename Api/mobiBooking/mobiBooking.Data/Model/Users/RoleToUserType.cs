using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model.Users
{
    public class RoleToUserType
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserTypeId { get; set; }
        public virtual Roles Role { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
