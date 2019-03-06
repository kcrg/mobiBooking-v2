﻿using System;
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
        public int UserType { get; set; }
        public int UserStatus { get; set; }
        public string Token { get; set; }
    }
}
