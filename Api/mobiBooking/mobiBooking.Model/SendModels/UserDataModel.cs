using mobiBooking.Data.Model;

namespace mobiBooking.Model.SendModels
{
    public class UserDataModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public int UserStatusId { get; set; }
        public string Token { get; set; }
    }
}
