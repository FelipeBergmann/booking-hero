using Booking.UnitTest.Common;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using BookingHero.UseCase.Faults;
using BookingHero.Booking.Core.UseCases.Reservation;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.Repositories;
using Moq;
using BookingHero.Booking.Core.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using BookingHero.Booking.Core.UseCases.Room.Validation;

namespace Booking.UnitTest.UseCase
{
    public class CancelReservationUseCaseTest : UseCaseBaseTest
    {
        private Mock<ILogger<CancelReservationUseCase>> _logger;

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _logger = new Mock<ILogger<CancelReservationUseCase>>();
        }

        private IEnumerable<Reservation> EmptyReservationEnumeration() => new List<Reservation>();

        [Test]
        public async Task ShouldCancelReservation()
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            string reservationCode = Reservation.GenerateReservationCode();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));
            roomRepository.Setup(r => r.FindReservation(roomId, null, reservationCode))
                          .Returns(Task.FromResult(new List<Reservation>()
                          {
                              new Reservation(Guid.NewGuid(), "", null, DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Confirmed, reservationCode)
                          }.AsEnumerable()));

            roomRepository.Setup(r => r.UpdateReservationStatus(It.IsAny<Guid>(), ReservationStatus.Canceled)).Verifiable("Update reservation status was not called");

            var validator = new CancelReservationValidator();
            var cancelReservationUseCase = new CancelReservationUseCase(_logger.Object, roomRepository.Object, validator);

            //Act
            var command = new CancelReservationCommand(roomId,reservationCode);
            await cancelReservationUseCase.Resolve(command);

            //Assert
            Assert.That(cancelReservationUseCase.IsFaulted, Is.False);

            roomRepository.Verify(r => r.UpdateReservationStatus(It.IsAny<Guid>(), ReservationStatus.Canceled), Times.Once);
        }

        [Test]
        public async Task ShouldNotCancelReservation()
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            string reservationCode = Reservation.GenerateReservationCode();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));
            roomRepository.Setup(r => r.FindReservation(roomId, null, reservationCode))
                          .Returns(Task.FromResult(EmptyReservationEnumeration()));

            var validator = new CancelReservationValidator();
            var cancelReservationUseCase = new CancelReservationUseCase(_logger.Object, roomRepository.Object, validator);

            //Act
            var command = new CancelReservationCommand(roomId,reservationCode);
            await cancelReservationUseCase.Resolve(command);

            //Assert
            Assert.That(cancelReservationUseCase.IsFaulted, Is.True);
            Assert.Greater(cancelReservationUseCase.GetErrors().Count(), 0);
            Assert.AreEqual(cancelReservationUseCase.GetErrors().FirstOrDefault()?.Code, UseCaseErrorType.BadRequest);
        }
    }
}
