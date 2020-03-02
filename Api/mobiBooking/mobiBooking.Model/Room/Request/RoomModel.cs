using System.ComponentModel.DataAnnotations;

namespace mobiBooking.Model.Models
{
    public class RoomModel
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int AvailabilityId { get; set; }
        [Required]
        public int NumberOfPeople { get; set; }
    }
}
