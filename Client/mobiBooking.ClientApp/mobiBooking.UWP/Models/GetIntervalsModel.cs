using Newtonsoft.Json;

namespace mobiBooking.UWP.Models
{
    internal class GetIntervalsModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}