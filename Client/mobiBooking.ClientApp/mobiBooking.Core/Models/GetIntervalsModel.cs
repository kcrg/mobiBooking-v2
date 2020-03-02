using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class GetIntervalsModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}