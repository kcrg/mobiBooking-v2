using Newtonsoft.Json;

namespace mobiBooking.Core.Models
{
    public class PostRoomsForRoomsReservationModel
    {
        [JsonProperty("size")]
        public int Size { get; set; } = 0;

        [JsonProperty("dateFrom")]
        public string DateFrom { get; set; } = "";

        [JsonProperty("dateTo")]
        public string DateTo { get; set; } = "";
    }
}