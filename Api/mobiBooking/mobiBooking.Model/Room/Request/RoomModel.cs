using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mobiBooking.Model.Models
{
    public class RoomModel
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Availability { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
    }
}
