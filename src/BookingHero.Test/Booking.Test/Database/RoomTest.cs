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
    internal class RoomTest : DbBaseTest
    {
        private IRoomRepository _roomRepository;

        private string GenerateReservationCode() => Guid.NewGuid().ToString().Substring(0, 8);

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _roomRepository = _serviceProvider.GetService<IRoomRepository>();
        }

        [TestCase("4c3e437a-9583-4252-a7c3-3d8971dd757b")]
        public async Task ShouldFindARoomById(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);

            Assert.That(room, Is.Not.Null);
            Assert.That(room.Id, Is.EqualTo(id));
        }
        
        [TestCase("303")]
        public async Task ShouldFindARoomByRoomNumber(string number)
        {
            var room = await _roomRepository.FindByNumber(number);

            Assert.That(room, Is.Not.Null);
            Assert.That(room.Number, Is.EqualTo(number));
        }
        
        [TestCase("4c3e437a-9583-4252-a7c3-3d8971dd757b", "27/08/2022")]
        public async Task ShouldListRoomConfirmedReservations(Guid roomId, string checkInDate)
        {
            var convertedCheckInDate = DateOnly.Parse(checkInDate);
            var reservations = await _roomRepository.FindConfirmedBookings(roomId, convertedCheckInDate);

            Assert.That(reservations, Is.Not.Null);
            Assert.True(reservations.Any());
            Assert.AreEqual(reservations.First().Room.Id, roomId);
        }

        
        [TestCase("4c3e437a-9583-4252-a7c3-3d8971dd757b", "27/08/2022", "30/08/2022", "email@email.com")]
        public async Task ShouldCreateConfirmedReservation(Guid roomId, string checkInDate, string checkOutDate, string customerEmail)
        {
            var checkIn = DateOnly.Parse(checkInDate);
            var checkOut = DateOnly.Parse(checkOutDate);
            var reservationId = Guid.NewGuid();
            var room = new Room(roomId, "The Room", "303");

            var reservation = new Reservation(reservationId, customerEmail, room, checkIn, checkOut, ReservationStatus.Confirmed, GenerateReservationCode());

            var foundRoom = await _roomRepository.GetByIdAsync(roomId);
            foundRoom.AddReservation(reservation);

            _roomRepository.Update(foundRoom);

            _roomRepository.SaveChanges();

            foundRoom = await _roomRepository.GetByIdAsync(roomId);
            Assert.IsTrue(foundRoom.Reservations.Where(x => x.Id == reservationId).Any());
        }
    }
}
