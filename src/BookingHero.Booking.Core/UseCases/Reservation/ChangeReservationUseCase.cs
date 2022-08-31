using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class ChangeReservationUseCase : UseCaseBase<ChangeReservationCommand, ReservationDto>, IChangeReservationUseCase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReserveRoomUseCase _reserveRoomUseCase;
        private readonly ICancelReservationUseCase _cancelReservationUseCase;
        public ChangeReservationUseCase(ILogger<ChangeReservationUseCase> logger,
                                        IRoomRepository roomRepository,
                                        IReserveRoomUseCase reserveRoomUseCase,
                                        ICancelReservationUseCase cancelReservationUseCase,
                                        ChangeReservationValidator validator) : base(logger, validator)
        {
            _roomRepository = roomRepository;
            _reserveRoomUseCase = reserveRoomUseCase;
            _cancelReservationUseCase = cancelReservationUseCase;
        }

        protected override async Task<ReservationDto> Execute(ChangeReservationCommand command)
        {
            var reservations = await _roomRepository.FindReservation(command.RoomId, null, command.ReservationCode);

            if (!reservations.Any())
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Reservation not found");

            var reservation = reservations?.FirstOrDefault();

            if (!reservation.CanChange())
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Reservation can not be changed");

            var reserveRoomCommand = new ReserveRoomCommand(command.RoomId, command.CheckIn, command.CheckOut, reservation.CustomerEmail);

            await _reserveRoomUseCase.Resolve(reserveRoomCommand);

            if (_reserveRoomUseCase.IsFaulted)
            {
                var error = _reserveRoomUseCase.GetErrors().FirstOrDefault();
                Fault(error.Code, error.Description);
            }

            await RunCancelReservationCommand(command.RoomId, command.ReservationCode);

            if (_cancelReservationUseCase.IsFaulted)
            {
                var error = _cancelReservationUseCase.GetErrors().FirstOrDefault();

                await RunCancelReservationCommand(command.RoomId, _reserveRoomUseCase.UseCaseResult.Code);

                if (_cancelReservationUseCase.IsFaulted)
                    Fault(UseCase.Faults.UseCaseErrorType.Unknown, $"We apologize! We reserved a new stay for you but we couldn't cancel the first reservation. Please, get in touch with the hotel as soon as possible. Your new reservation code is {_reserveRoomUseCase.UseCaseResult.Code}");

                Fault(error.Code, error.Description);
            }

            return _reserveRoomUseCase.UseCaseResult;
        }

        private async Task RunCancelReservationCommand(Guid roomId, string reservationCode)
        {
            var cancelReservationCommand = new CancelReservationCommand(roomId, reservationCode);
            await _cancelReservationUseCase.Resolve(cancelReservationCommand);
        }
    }
}
