using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Room
{
    public class ListRoomUseCase : UseCaseBase<ListRoomCommand, IEnumerable<RoomDto>>, IListRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public ListRoomUseCase(ILogger<ListRoomUseCase> logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<IEnumerable<RoomDto>> Execute(ListRoomCommand command)
        {
            var rooms = _roomRepository.Find(x =>
                    (string.IsNullOrEmpty(command.Number) || x.Number == command.Number));
            return rooms.Select(x => (RoomDto)x);
        }
    }
}
