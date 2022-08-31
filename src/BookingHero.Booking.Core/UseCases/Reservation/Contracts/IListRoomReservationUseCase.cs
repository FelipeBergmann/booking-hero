using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public interface IListRoomReservationUseCase : IUseCase<ListRoomReservationCommand, RoomReservationDto> 
    {
    }
}