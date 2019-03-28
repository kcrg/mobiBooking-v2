using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class RoomAvailability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursFrom { get; set; }
        public int HoursTo { get; set; }
        public virtual IEnumerable<Room> Rooms { get; set; }
    }
}
