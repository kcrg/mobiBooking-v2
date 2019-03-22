using Newtonsoft.Json;
using System.Collections.Generic;

namespace mobiBooking.UWP.Models
{
    internal class BookModel
    {
        [JsonProperty("roomId")]
        public int RoomId { get; set; }

        [JsonProperty("dateFrom")]
        public string DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public string DateTo { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("invitedUsersIds")]
        public IList<int> InvitedUsersIds { get; set; }

        [JsonProperty("cyclicReservation")]
        public bool CyclicReservation { get; set; }

        [JsonProperty("reservationIntervalId")]
        public int ReservationIntervalId { get; set; }

        public List<int> usersListInt { get; set; }
    }
}