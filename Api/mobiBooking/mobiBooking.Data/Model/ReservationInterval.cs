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

    public enum Intervals
    {
        Day,
        Week,
        TwoWeeks,
        Month,
        Year
    }
}
