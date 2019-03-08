using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.RecivedModels
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public int UserStatusId { get; set; }
    }
}
