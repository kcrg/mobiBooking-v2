using Newtonsoft.Json;

namespace mobiBooking.UWP.Models
{
    internal class SignInModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}