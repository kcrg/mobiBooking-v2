namespace mobiBooking.UWP.Models
{
    internal class AddRoomModel
    {
        public string RoomName { get; set; }
        public string Location { get; set; }
        public bool Activity { get; set; }
        public int Availability { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Flipchart { get; set; }
        public bool SoundSystem { get; set; }
    }
}