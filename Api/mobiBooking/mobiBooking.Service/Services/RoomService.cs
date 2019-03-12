using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mobiBooking.Service.Services
{
    public class RoomService : IRoomService
    {

        private readonly IRepositoryWrapper _repositoryWrapper;

        public RoomService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public bool Create(RoomModel value)
        {

            if ( string.IsNullOrEmpty(value.Availability)
                || string.IsNullOrEmpty(value.Location)
                || string.IsNullOrEmpty(value.RoomName)
                || value.Activity == null              
                || value.NumberOfPeople == null)
            {
                return false;
            }

            _repositoryWrapper.Room.Create(new Room
            {
                Activity = (bool)value.Activity,
                Availability = value.Availability,
                Location = value.Location,
                Name = value.RoomName,
                NumberOfPeople = (int)value.NumberOfPeople
            });
            _repositoryWrapper.Room.Save();

            return true;
        }

        public bool Delete(int id)
        {
            Room room = _repositoryWrapper.Room.Find(id);

            if (room == null)
                return false;

            _repositoryWrapper.Room.Delete(room);
            return true;
        }

        public RoomModel Get(int id)
        {
            Room room = _repositoryWrapper.Room.Find(id);

            if (room == null)
                return null;

            return new RoomModel
            {
                Activity = room.Activity,
                Availability = room.Availability,
                Location = room.Location,
                NumberOfPeople = room.NumberOfPeople,
                RoomName = room.Name
            };
        }

        public IEnumerable<RoomModel> GetAll()
        {
            List<RoomModel> roomModels = new List<RoomModel>();
            _repositoryWrapper.Room.FindAll().ToList().ForEach((room) =>
            {
                roomModels.Add(new RoomModel
                {
                    Activity = room.Activity,
                    Availability = room.Availability, 
                    Location = room.Location,
                    NumberOfPeople = room.NumberOfPeople,
                    RoomName = room.Name
                });
            });

            return roomModels;
        }

        public void Update(int id, RoomModel value)
        {
            Room room = _repositoryWrapper.Room.Find(id);

            room.Activity = value.Activity ?? room.Activity;
            room.Availability = value.Availability ?? room.Availability;
            room.Location = value.Location ?? room.Location;
            room.Name = value.RoomName ?? room.Name;
            room.NumberOfPeople = value.NumberOfPeople ?? room.NumberOfPeople;

            _repositoryWrapper.Room.Update(room);
            _repositoryWrapper.Room.Save();
        }
    }
}
