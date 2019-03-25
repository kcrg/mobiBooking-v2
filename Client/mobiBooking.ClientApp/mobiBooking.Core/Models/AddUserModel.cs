using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class AddUserModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        public int UserID { get; set; }
    }
}