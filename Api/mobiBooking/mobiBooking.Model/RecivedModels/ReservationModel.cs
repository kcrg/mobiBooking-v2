using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.RecivedModels
{
    public class ReservationModel
    {
        public int RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public IEnumerable<int> InvitedUsersIds { get; set; }
    }
}
