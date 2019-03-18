using System;
using System.Collections.Generic;

namespace mobiBooking.UWP.Models
{
    internal class BookModel
    {
        public int RoomId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public List<int> InvitedUsersIds { get; set; }
        public bool CyclicReservation { get; set; }
        public int ReservationIntervalId { get; set; }
    }
}