using mobiBooking.Component.Enums;
using mobiBooking.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mobiBooking.Model.RecivedModels
{
    public class ReservationModel
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        [EnumDataType(typeof(ReservationStatus), ErrorMessage = "ReservationStatus type value doesn't exist within enum")]
        public ReservationStatus Status { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IEnumerable<int> InvitedUsersIds { get; set; }
        [Required]
        public bool? CyclicReservation { get; set; }
        public int? ReservationIntervalId { get; set; }
    }
}
