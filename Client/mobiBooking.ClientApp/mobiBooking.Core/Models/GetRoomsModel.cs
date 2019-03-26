using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class GetRoomsModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("availability")]
        public string Availability { get; set; }

        [JsonProperty("availabilityId")]
        public int AvailabilityId { get; set; }

        [JsonProperty("numberOfPeople")]
        public int NumberOfPeople { get; set; }
    }
}