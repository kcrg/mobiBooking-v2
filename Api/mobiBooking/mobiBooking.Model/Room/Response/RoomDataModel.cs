using System;
using System.Collections.Generic;
using System.Text;

namespace mobiBooking.Model.SendModels
{
    public class RoomDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Activity { get; set; }
        public string Availability { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Flipchart { get; set; }
        public bool SoundSystem { get; set; }
    }
}
