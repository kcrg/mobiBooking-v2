using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.Room.Response
{
    public class RoomDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Availability { get; set; }
        public int AvailabilityId { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
