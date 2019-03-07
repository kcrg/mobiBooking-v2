using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace mobiBooking.Data.Model.Rooms
{ 
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Activity { get; set; }
        public string Availability { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
