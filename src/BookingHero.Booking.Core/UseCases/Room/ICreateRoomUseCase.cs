using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;

namespace BookingHero.Booking.Core.UseCases.Room
{
    public interface ICreateRoomUseCase : IUseCase<CreateRoomCommand, RoomDto>{}
}
