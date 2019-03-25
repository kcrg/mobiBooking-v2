using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class GetUsersModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        public string ActiveString { get; set; }
    }
}