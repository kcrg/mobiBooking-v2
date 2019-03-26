using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class SignInModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}