using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
    }

    public enum UserType
    {
        Administrator,
        User
    }

    public enum UserStatus
    {
        Active,
        Inactive
    }
}
