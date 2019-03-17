using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.Room.Request
{
    public class RoomsForReservationModel
    {
        public int Size { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool FlipChart { get; set; }
        public bool SoundSystem { get; set; }
    }
}
