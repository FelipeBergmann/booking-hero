using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Room
{
    public class CreateRoomUseCase : UseCaseBase<CreateRoomCommand, RoomDto>, ICreateRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public CreateRoomUseCase(ILogger<CreateRoomUseCase> logger, IRoomRepository roomRepository, CreateRoomValidator validator) : base(logger, validator)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<RoomDto> Execute(CreateRoomCommand command)
        {
            var room = new Entities.Room(Guid.NewGuid(), command.Name, command.Number);

            _roomRepository.Add(room);
            _roomRepository.SaveChanges();

            return room!;
        }
    }
}
