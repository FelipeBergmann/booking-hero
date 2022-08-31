using Booking.UnitTest.Common;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using BookingHero.Booking.Core.UseCases.Reservation;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.Repositories;
using Moq;
using BookingHero.Booking.Core.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Booking.UnitTest.UseCase
{
    public class GetReservationUseCaseTest : UseCaseBaseTest
    {
        private ILogger _logger;

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _logger = _serviceProvider.GetService<ILogger>()!;
        }

        [TestCase("D994184C-F976-4E4C-ACBB-B6B050CA0560", "D994184C", Description = "Should get reservation filtering by id and reservation code")]
        [TestCase("D994184C-F976-4E4C-ACBB-B6B050CA0560", null, Description = "Should get reservation filtering by id only")]
        [TestCase(null, "D994184C", Description = "Should get reservation filtering by code only")]
        public async Task ShouldGetRervation(Guid? reservationId, string? reservationCode)
        {
            //Arrange
            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.FindReservation(reservationId, reservationCode))
                          .Returns(Task.FromResult(
                              new Reservation(Guid.NewGuid(), "", new Room(Guid.NewGuid(), "room", "303"), DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Confirmed, reservationCode)))
                          .Verifiable();

            var getReservationUseCase = new GetReservationUseCase(_logger, roomRepository.Object);

            //Act
            var command = new GetReservationCommand()
            {
                Id = reservationId,
                Code = reservationCode
            };
            await getReservationUseCase.Resolve(command);

            //Assert
            Assert.That(getReservationUseCase.IsFaulted, Is.False);
            Assert.That(getReservationUseCase.UseCaseResult, Is.Not.Null);

            roomRepository.Verify(r => r.FindReservation(reservationId, reservationCode), Times.Once);
        }
    }
}
