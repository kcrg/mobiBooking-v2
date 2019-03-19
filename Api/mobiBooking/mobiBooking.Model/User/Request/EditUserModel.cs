﻿using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.User.Request
{
    public class EditUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool? Active { get; set; }
    }
}