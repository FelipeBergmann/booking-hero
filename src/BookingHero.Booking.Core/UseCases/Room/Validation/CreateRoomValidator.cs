using BookingHero.Booking.Core.UseCases.Commands.Room;
using FluentValidation;

namespace BookingHero.Booking.Core.UseCases.Room.Validation
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .WithMessage((cmd, prop) => $"can not be empty")
                                .NotEmpty()
                                .WithMessage((cmd, prop) => $"can not be empty")
                                .MaximumLength(60)
                                .WithMessage((cmd, prop) => $"maximium length is 60");

            RuleFor(x => x.Number).NotNull()
                                .WithMessage((cmd, prop) => $"can not be empty")
                                .NotEmpty()
                                .WithMessage((cmd, prop) => $"can not be empty")
                                .MaximumLength(6)
                                .WithMessage((cmd, prop) => $"maximium length is 6");
        }
    }
}
