using mobiBooking.Component.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Data.Model
{
    public class ReservationInterval
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Intervals Time { get; set; }
    }
}
