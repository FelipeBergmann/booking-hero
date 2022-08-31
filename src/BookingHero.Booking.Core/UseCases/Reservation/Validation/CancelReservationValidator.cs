using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using FluentValidation;

namespace BookingHero.Booking.Core.UseCases.Room.Validation
{
    public class CancelReservationValidator : AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null");

            RuleFor(x => x.ReservationCode).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null");     
        }
    }
}
