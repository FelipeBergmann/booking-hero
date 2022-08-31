using Booking.UnitTest.Common;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Room;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using BookingHero.UseCase.Faults;
using Microsoft.Extensions.Logging;
using Moq;
using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.Entities;
using System.Linq.Expressions;
using System.Collections.Generic;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using BookingHero.Booking.Core.UseCases.Dto;

namespace Booking.UnitTest.UseCase
{
    public class RoomUseCaseTest : UseCaseBaseTest
    {
        [OneTimeSetUp]
        public void InitEnvironment()
        {
        }

        [Test]
        public async Task ShouldGetRoom()
        {
            Guid roomId = Guid.NewGuid();
            var logger = new Mock<ILogger<GetRoomUseCase>>();
            var validator = new GetRoomValidator();
            var roomRepository = new Mock<IRoomRepository>();

            roomRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                          .Returns(() =>
                          {
                              return Task.FromResult(new Room(roomId, "any name", "1010"));
                          });


            var getRoomUseCase = new GetRoomUseCase(logger.Object, roomRepository.Object, validator);
            await getRoomUseCase.Resolve(new GetRoomCommand(roomId));

            Assert.That(getRoomUseCase.IsFaulted, Is.False);
            Assert.AreEqual(getRoomUseCase.UseCaseResult.Id, roomId);
        }

        [Test]
        public async Task ShouldNotFindRoom()
        {
            Guid roomId = Guid.NewGuid();
            var logger = new Mock<ILogger<GetRoomUseCase>>();
            var validator = new GetRoomValidator();
            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

            var getRoomUseCase = new GetRoomUseCase(logger.Object, roomRepository.Object, validator);
            await getRoomUseCase.Resolve(new GetRoomCommand(roomId));

            var errors = getRoomUseCase.GetErrors().FirstOrDefault();

            Assert.That(getRoomUseCase.IsFaulted, Is.True);
            Assert.AreEqual(errors?.Code, UseCaseErrorType.BadRequest);
        }

        [TestCase("303", true)]
        [TestCase("", true)]
        [TestCase("1200", false)]
        public async Task ShouldListRooms(string roomNumber, bool expectedHasAny)
        {
            var logger = new Mock<ILogger<ListRoomUseCase>>();
            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Room, bool>>>()))
                          .Returns(() =>
                          {
                              if (!expectedHasAny)
                                  return new List<Room>();

                              var roomsList = new List<Room>()
                              {
                                  new Room(Guid.NewGuid(), "any name", "123")
                              };

                              return roomsList.AsEnumerable();
                          });

            var listRoomUseCase = new ListRoomUseCase(logger.Object, roomRepository.Object);
            await listRoomUseCase.Resolve(new ListRoomCommand()
            {
                Number = roomNumber
            });

            Assert.That(listRoomUseCase.IsFaulted, Is.False);
            Assert.AreEqual(listRoomUseCase.UseCaseResult.Any(), expectedHasAny);
        }
    }
}
