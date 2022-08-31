using BookingHero.Booking.Core.UseCases.Commands.Room;
using FluentValidation;

namespace BookingHero.Booking.Core.UseCases.Room.Validation
{
    public class GetRoomValidator : AbstractValidator<GetRoomCommand>
    {
        public GetRoomValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                              .WithMessage("Search parameter can not be empty");
        }
    }
}
