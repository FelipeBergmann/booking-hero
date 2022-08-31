using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using FluentValidation;

namespace BookingHero.Booking.Core.UseCases.Room.Validation
{
    public class ChangeReservationValidator : AbstractValidator<ChangeReservationCommand>
    {
        public ChangeReservationValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null");

            RuleFor(x => x.ReservationCode).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null");

            RuleFor(x => x.CheckIn).NotEmpty()
                                 .WithMessage("Cannot be empty")
                                 .NotNull()
                                 .WithMessage("Cannot be null")
                                 .LessThanOrEqualTo(x => x.CheckOut)
                                 .WithMessage("Cannot be later than Check in date"); ;

            RuleFor(x => x.CheckOut).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null")
                                  .GreaterThanOrEqualTo(x => x.CheckIn)
                                  .WithMessage("Cannot be earlier than Check in date");
        }
    }
}
