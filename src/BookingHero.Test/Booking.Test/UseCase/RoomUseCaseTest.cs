using Booking.UnitTest.Common;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Room;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using BookingHero.UseCase.Faults;

namespace Booking.UnitTest.UseCase
{
    public class RoomUseCaseTest : UseCaseBaseTest
    {
        private IGetRoomUseCase _getRoomUseCase;
        private IListRoomUseCase _listRoomUseCase;

        [OneTimeSetUp]
        public void InitEnvironment()
        {
            _getRoomUseCase = _serviceProvider.GetService<IGetRoomUseCase>()!;
            _listRoomUseCase = _serviceProvider.GetService<IListRoomUseCase>()!;
        }

        [TestCase("4c3e437a-9583-4252-a7c3-3d8971dd757b")]
        public async Task ShouldGetRoom(Guid roomId)
        {
            await _getRoomUseCase.Resolve(new GetRoomCommand(roomId));

            Assert.That(_getRoomUseCase.IsFaulted, Is.False);
            Assert.AreEqual(_getRoomUseCase.UseCaseResult.Id, roomId);
        }

        [TestCase]
        public async Task ShouldNotFindRoom()
        {
            await _getRoomUseCase.Resolve(new GetRoomCommand(Guid.NewGuid()));
            var errors = _getRoomUseCase.GetErrors().FirstOrDefault();

            Assert.That(_getRoomUseCase.IsFaulted, Is.True);
            Assert.AreEqual(errors?.Code, UseCaseErrorType.BadRequest);
        }

        [TestCase("303", true)]
        [TestCase("", true)]
        [TestCase("1200", false)]
        public async Task ShouldListRooms(string roomNumber, bool expectedHasAny)
        {
            await _listRoomUseCase.Resolve(new ListRoomCommand()
            {
                Number = roomNumber
            });

            Assert.That(_listRoomUseCase.IsFaulted, Is.False);
            Assert.AreEqual(_listRoomUseCase.UseCaseResult.Any(), expectedHasAny);
        }
    }
}
