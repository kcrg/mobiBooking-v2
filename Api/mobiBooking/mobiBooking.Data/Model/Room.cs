using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace mobiBooking.Data.Model
{ 
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Activity { get; set; }
        public int AvailabilityId { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Flipchart { get; set; }
        public bool SoundSystem { get; set; }

        public virtual RoomAvailability Availability { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
