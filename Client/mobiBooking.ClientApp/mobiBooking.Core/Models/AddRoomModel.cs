using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class AddRoomModel
    {
        [JsonProperty("roomName")]
        public string RoomName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("availability")]
        public int Availability { get; set; }

        [JsonProperty("numberOfPeople")]
        public int NumberOfPeople { get; set; }
    }
}