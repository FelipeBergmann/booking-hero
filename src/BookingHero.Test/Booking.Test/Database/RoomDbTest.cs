using Booking.UnitTest.Common;
using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.UnitTest.Database
{
    internal class RoomDbTest : DbBaseTest
    {
        private IRoomRepository _roomRepository;

        private string GenerateReservationCode() => Guid.NewGuid().ToString().Substring(0, 8);

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _roomRepository = _serviceProvider.GetService<IRoomRepository>();
        }

        private Room InsertNewRoom()
        {
            var newRoom = new Room(Guid.NewGuid(), "Test room", "101");
            _roomRepository.Add(newRoom);
            _roomRepository.SaveChanges();

            return newRoom;
        }

        private void RemoveRoom(Room room)
        {
            _roomRepository.Remove(room);
            _roomRepository.SaveChanges();
        }

        [Test]
        public async Task ShouldFindARoomById()
        {
            var newRoom = InsertNewRoom();

            var room = await _roomRepository.GetByIdAsync(newRoom.Id);

            Assert.That(room, Is.Not.Null);
            Assert.That(room.Id, Is.EqualTo(newRoom.Id));

            RemoveRoom(newRoom);
        }

        [Test]
        public async Task ShouldFindARoomByRoomNumber()
        {
            var newRoom = InsertNewRoom();

            var room = await _roomRepository.FindByNumber("101");

            Assert.That(room, Is.Not.Null);
            Assert.That(room.Number, Is.EqualTo("101"));

            RemoveRoom(newRoom);
        }

        [Test]
        public async Task ShouldListRoomConfirmedReservations()
        {
            var newRoom = InsertNewRoom();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            DateOnly checkOut = checkIn.AddDays(3);

            newRoom.AddReservation(new Reservation(Guid.NewGuid(), "test@test.com", newRoom, checkIn, checkOut, ReservationStatus.Confirmed, "code"));

            _roomRepository.Update(newRoom);
            _roomRepository.SaveChanges();

            var reservations = await _roomRepository.FindRoomReservationForCheckInDate(newRoom.Id, checkIn);

            Assert.That(reservations, Is.Not.Null);
            Assert.True(reservations.Any());
            Assert.AreEqual(reservations.First().Room.Id, newRoom.Id);

            RemoveRoom(newRoom);
        }


        [TestCase("27/08/2022", "30/08/2022", "email@email.com")]
        public async Task ShouldCreateConfirmedReservation(string checkInDate, string checkOutDate, string customerEmail)
        {
            var newRoom = InsertNewRoom();

            var checkIn = DateOnly.Parse(checkInDate);
            var checkOut = DateOnly.Parse(checkOutDate);
            var reservationId = Guid.NewGuid();

            var reservation = new Reservation(reservationId, customerEmail, newRoom, checkIn, checkOut, ReservationStatus.Confirmed, GenerateReservationCode());

            var foundRoom = await _roomRepository.GetByIdAsync(newRoom.Id);
            foundRoom.AddReservation(reservation);

            _roomRepository.Update(foundRoom);

            _roomRepository.SaveChanges();

            foundRoom = await _roomRepository.GetByIdAsync(newRoom.Id);
            Assert.IsTrue(foundRoom.Reservations.Where(x => x.Id == reservationId).Any());

            RemoveRoom(newRoom);
        }
    }
}
