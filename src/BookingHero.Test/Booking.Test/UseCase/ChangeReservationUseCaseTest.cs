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
    public class ChangeReservationUseCaseTest : UseCaseBaseTest
    {
        private Mock<ILogger<ChangeReservationUseCase>> _logger;

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _logger = new Mock<ILogger<ChangeReservationUseCase>>();
        }

        private IEnumerable<Reservation> EmptyReservationEnumeration() => new List<Reservation>();

        [Test]
        public async Task ShouldChangeReservation()
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            string reservationCode = Reservation.GenerateReservationCode();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            DateOnly checkOut = checkIn.AddDays(2);

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.FindReservation(roomId, null, reservationCode))
                          .Returns(() =>
                          {
                              var listResult = new List<Reservation>()
                              {
                                  new Reservation(roomId, "", null, DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Confirmed, reservationCode)
                              };

                              return Task.FromResult(listResult.AsEnumerable());
                          })
                          .Verifiable();

            Guid newReservationId = Guid.NewGuid();
            var reserveRoomUseCaseMock = new Mock<IReserveRoomUseCase>();
            reserveRoomUseCaseMock.Setup(x => x.Resolve(It.IsAny<ReserveRoomCommand>()));
            reserveRoomUseCaseMock.Setup(x => x.UseCaseResult)
                                  .Returns(() => new BookingHero.Booking.Core.UseCases.Dto.ReservationDto()
                                  {
                                      Id = newReservationId,
                                      Code = Reservation.GenerateReservationCode(),
                                      CheckIn = checkIn,
                                      CheckOut = checkOut,
                                      Status = ReservationStatus.Confirmed
                                  });

            var cancelReservationUseCaseMock = new Mock<ICancelReservationUseCase>();
            cancelReservationUseCaseMock.Setup(x => x.Resolve(It.IsAny<CancelReservationCommand>()));

            var validator = new ChangeReservationValidator();
            var changeReservationUseCase = new ChangeReservationUseCase(_logger.Object, roomRepository.Object, reserveRoomUseCaseMock.Object, cancelReservationUseCaseMock.Object, validator);

            //Act
            var command = new ChangeReservationCommand(roomId, reservationCode)
            {
                CheckIn = checkIn,
                CheckOut = checkOut
            };
            await changeReservationUseCase.Resolve(command);

            //Assert
            Assert.That(changeReservationUseCase.IsFaulted, Is.False);
            Assert.That(changeReservationUseCase.UseCaseResult, Is.Not.Null);
            Assert.AreEqual(changeReservationUseCase.UseCaseResult.Id, newReservationId);
        }

        [Test]
        public async Task ShouldNotFindReservation()
        {
            //arrange
            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.FindReservation(It.IsAny<Guid>(), null, It.IsAny<string>()))
                          .Returns(() => Task.FromResult((new List<Reservation>()).AsEnumerable()));

            var reserveRoomUseCaseMock = new Mock<IReserveRoomUseCase>();
            var cancelReservationUseCaseMock = new Mock<ICancelReservationUseCase>();
            var validator = new ChangeReservationValidator();
            var changeReservationUseCase = new ChangeReservationUseCase(_logger.Object, roomRepository.Object, reserveRoomUseCaseMock.Object, cancelReservationUseCaseMock.Object, validator);

            //Act
            var command = new ChangeReservationCommand(Guid.NewGuid(), "CODE");
            await changeReservationUseCase.Resolve(command);

            Assert.That(changeReservationUseCase.IsFaulted, Is.True);
            Assert.AreEqual(changeReservationUseCase.GetErrors().First().Code, UseCaseErrorType.BadRequest);
        }

        [Test]
        public async Task ShouldTryChangeCanceledReservation()
        {
            //arrange
            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.FindReservation(It.IsAny<Guid>(), null, It.IsAny<string>()))
                          .Returns(() => Task.FromResult((new List<Reservation>()
                          {
                              new Reservation(Guid.NewGuid(), "", null, DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Canceled, "")
                          }).AsEnumerable()));

            var reserveRoomUseCaseMock = new Mock<IReserveRoomUseCase>();
            var cancelReservationUseCaseMock = new Mock<ICancelReservationUseCase>();
            var validator = new ChangeReservationValidator();
            var changeReservationUseCase = new ChangeReservationUseCase(_logger.Object, roomRepository.Object, reserveRoomUseCaseMock.Object, cancelReservationUseCaseMock.Object, validator);

            //Act
            var command = new ChangeReservationCommand(Guid.NewGuid(), "CODE");
            await changeReservationUseCase.Resolve(command);

            Assert.That(changeReservationUseCase.IsFaulted, Is.True);
            Assert.AreEqual(changeReservationUseCase.GetErrors().First().Code, UseCaseErrorType.BadRequest);
        }

    }
}
