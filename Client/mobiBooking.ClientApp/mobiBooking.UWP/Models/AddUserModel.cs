namespace mobiBooking.UWP.Models
{
    internal class AddUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}