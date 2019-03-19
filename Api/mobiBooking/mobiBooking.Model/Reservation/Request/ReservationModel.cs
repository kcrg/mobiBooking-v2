using mobiBooking.Component.Enums;
using mobiBooking.Data.Model;
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
        public ReservationStatus Status { get; set; }
        public string Title { get; set; }
        public IEnumerable<int> InvitedUsersIds { get; set; }
        public bool? CyclicReservation { get; set; }
        public int? ReservationIntervalId { get; set; }
    }
}
