namespace mobiBooking.UWP.Models
{
    internal class LoginModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public bool IsLoged { get; set; }  = false;
    }
}