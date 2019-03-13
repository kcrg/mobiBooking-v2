using System;
using System.Collections.Generic;

namespace mobiBooking.UWP.Models
{
    internal class BookModel
    {
        public int RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public List<int> InvitedUsersIds { get; set; }
        public string Token { get; set; }
    }
}