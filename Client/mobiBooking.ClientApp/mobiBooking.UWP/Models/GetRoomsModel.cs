namespace mobiBooking.UWP.Models
{
    internal class GetRoomsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Activity { get; set; }
        public string Availability { get; set; }
        public int NumberOfPeople { get; set; }
    }
}