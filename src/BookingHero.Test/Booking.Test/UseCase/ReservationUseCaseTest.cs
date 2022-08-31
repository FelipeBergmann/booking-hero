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
    public class ReservationUseCaseTest : UseCaseBaseTest
    {
        private Mock<ILogger<ReserveRoomUseCase>> _logger;

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _logger = new Mock<ILogger<ReserveRoomUseCase>>();
        }

        private IEnumerable<Reservation> EmptyReservationEnumeration() => new List<Reservation>();

        [Test]
        public async Task ShouldCanReserveARoom()
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            DateOnly checkOut = checkIn.AddDays(2);
            int FindRoomReservationForCheckInDateCalledTimes = 0;

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));
            roomRepository.Setup(r => r.FindRoomReservationForCheckInDate(roomId, checkIn))
                          .Callback(() => ++FindRoomReservationForCheckInDateCalledTimes)
                          .Returns(() => {
                              if (FindRoomReservationForCheckInDateCalledTimes <= 1)
                              {
                                  return Task.FromResult(EmptyReservationEnumeration());
                              }

                              var listResult = new List<Reservation>()
                              {
                                  new Reservation(Guid.Parse(roomRepository.Invocations[roomRepository.Invocations.Count() - 1].Arguments[0].ToString()!), "", null, DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Confirmed, null)
                              };

                              return Task.FromResult(listResult.AsEnumerable());
                          })
                          .Verifiable();
            roomRepository.Setup(r => r.Update(It.IsAny<Room>())).Verifiable("Repository Update was not called");
            roomRepository.Setup(r => r.SaveChanges()).Verifiable("Repository SaveChanges was not called");

            var validator = new RoomReservationValidator();
            var reserveRoomUseCase = new ReserveRoomUseCase(_logger.Object, roomRepository.Object, validator);
 
            //Act
            var command = new ReserveRoomCommand(roomId, checkIn, checkOut, "customerEmail@email.com");
            await reserveRoomUseCase.Resolve(command);

            //Assert
            Assert.That(reserveRoomUseCase.IsFaulted, Is.False);
            Assert.That(reserveRoomUseCase.UseCaseResult, Is.Not.Null);

            roomRepository.Verify(r => r.FindRoomReservationForCheckInDate(roomId, checkIn), Times.Exactly(2));
            roomRepository.Verify(r => r.Update(It.IsAny<Room>()), Times.Once);
            roomRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task ShouldTryToReserveReservedARoom()
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
            DateOnly checkOut = checkIn.AddDays(2);

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));
            roomRepository.Setup(r => r.FindRoomReservationForCheckInDate(roomId, checkIn))
                          .Returns(() => Task.FromResult(new List<Reservation>()
                                        {
                                            new Reservation(Guid.NewGuid(), "", null, DateOnly.MinValue, DateOnly.MinValue, ReservationStatus.Confirmed, null)
                                        }.AsEnumerable()))
                          .Verifiable();
            roomRepository.Setup(r => r.Update(It.IsAny<Room>())).Verifiable("Repository Update was not called");
            roomRepository.Setup(r => r.SaveChanges()).Verifiable("Repository SaveChanges was not called");

            var validator = new RoomReservationValidator();
            var reserveRoomUseCase = new ReserveRoomUseCase(_logger.Object, roomRepository.Object, validator);

            //Act
            var command = new ReserveRoomCommand(roomId, checkIn, checkOut, "customerEmail@email.com");
            await reserveRoomUseCase.Resolve(command);

            //Assert
            Assert.That(reserveRoomUseCase.IsFaulted, Is.True);
            Assert.That(reserveRoomUseCase.UseCaseResult, Is.Null);
            Assert.Greater(reserveRoomUseCase.GetErrors().Count(), 0);
            Assert.AreEqual(reserveRoomUseCase.GetErrors().First().Code, UseCaseErrorType.BadRequest);

            roomRepository.Verify(r => r.FindRoomReservationForCheckInDate(roomId, checkIn), Times.Once);            
        }

        [TestCase(31, 33, Description = "Should not allow to reserve in more than 30 days in advance")]
        [TestCase(1, 4, Description = "Should not allow to reserve for more than the allowed period")]
        [TestCase(0, 3, Description = "Should not allow to reserve for check in on the same date")]
        public async Task ShouldNotReserve(int checkInAddDays, int checkoutAddDays)
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(checkInAddDays);
            DateOnly checkOut = DateOnly.FromDateTime(DateTime.Today).AddDays(checkoutAddDays);

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));

            var validator = new RoomReservationValidator();
            var reserveRoomUseCase = new ReserveRoomUseCase(_logger.Object, roomRepository.Object, validator);

            //Act
            var command = new ReserveRoomCommand(roomId, checkIn, checkOut, "customerEmail");
            await reserveRoomUseCase.Resolve(command);

            //Assert
            Assert.That(reserveRoomUseCase.IsFaulted, Is.True);
            Assert.That(reserveRoomUseCase.UseCaseResult, Is.Null);
            Assert.Greater(reserveRoomUseCase.GetErrors().Count(), 0);
            Assert.AreEqual(reserveRoomUseCase.GetErrors().First().Code, UseCaseErrorType.BadRequest);            
        }

        [TestCase(2, 3, Description = "Should reserve check in in two days for one day")]
        [TestCase(1, 3, Description = "Should reserve in one day for three days")]
        public async Task ShouldReserveWithValidStayDates(int checkInAddDays, int checkoutAddDays)
        {
            //Arrange
            Guid roomId = Guid.NewGuid();
            DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today).AddDays(checkInAddDays);
            DateOnly checkOut = DateOnly.FromDateTime(DateTime.Today).AddDays(checkoutAddDays);

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetByIdAsync(roomId)).Returns(Task.FromResult(new Room(roomId, "The room", "303")));

            var validator = new RoomReservationValidator();
            var reserveRoomUseCase = new ReserveRoomUseCase(_logger.Object, roomRepository.Object, validator);

            //Act
            var command = new ReserveRoomCommand(roomId, checkIn, checkOut, "customerEmail@email.com");
            await reserveRoomUseCase.Resolve(command);

            //Assert
            Assert.That(reserveRoomUseCase.IsFaulted, Is.False);
            Assert.That(reserveRoomUseCase.UseCaseResult, Is.Not.Null);
            Assert.AreEqual(reserveRoomUseCase.GetErrors().Count(), 0);            
        }
    }
}
