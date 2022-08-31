using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using FluentValidation;

namespace BookingHero.Booking.Core.UseCases.Room.Validation
{
    public class ListRoomReservationValidator : AbstractValidator<ListRoomReservationCommand>
    {
        public ListRoomReservationValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty()
                                  .WithMessage("Cannot be empty")
                                  .NotNull()
                                  .WithMessage("Cannot be null");

            RuleFor(x => x.CheckIn).NotEmpty()
                                 .WithMessage("Cannot be empty")
                                 .NotNull()
                                 .WithMessage("Cannot be null");
        }
    }
}
