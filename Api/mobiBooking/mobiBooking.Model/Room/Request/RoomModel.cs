using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public string Location { get; set; }
        public bool? Activity { get; set; }
        public int? Availability { get; set; }
        public int? NumberOfPeople { get; set; }
        public bool? Flipchart { get; set; }
        public bool? SoundSystem { get; set; }
    }
}
