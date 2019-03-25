namespace mobiBooking.Core.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string surName { get; set; }
        public string Email { get; set; }
        public string role { get; set; }
        public string Token { get; set; }
        public bool IsLoged { get; set; }  = false;
    }
}