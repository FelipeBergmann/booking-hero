using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.UseCase;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public interface ICancelReservationUseCase : IUseCase<CancelReservationCommand> 
    {
    }
}