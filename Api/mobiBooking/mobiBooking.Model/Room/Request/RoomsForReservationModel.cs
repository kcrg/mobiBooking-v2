using System;
using System.ComponentModel.DataAnnotations;

namespace mobiBooking.Model.Room.Request
{
    public class RoomsForReservationModel
    {
        [Required]
        public int Size { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
    }
}
