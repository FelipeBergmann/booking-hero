using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class GetReservationUseCase : UseCaseBase<GetReservationCommand, ReservationDto>, IGetReservationUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public GetReservationUseCase(ILogger<GetReservationUseCase> logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<ReservationDto> Execute(GetReservationCommand command)
        {
            var reservation = await _roomRepository.FindReservation(command.Id, command.Code);

            if (reservation == null)
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Reservation not found");

            return reservation!;
        }
    }
}
