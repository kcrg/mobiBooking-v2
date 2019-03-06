using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string  Title { get; set; }
        public int Status { get; set; }
    }
}
