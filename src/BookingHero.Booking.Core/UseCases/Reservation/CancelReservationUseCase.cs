﻿using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;
using BookingHero.Booking.Core.Utils.Extensions;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class CancelReservationUseCase : UseCaseBase<CancelReservationCommand>, ICancelReservationUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public CancelReservationUseCase(ILogger logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task Execute(CancelReservationCommand command)
        {
            var room = await _roomRepository.GetByIdAsync(command.RoomId);

            if (room == null)
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Room not found");

            var reservations = await _roomRepository.FindReservation(command.RoomId, null, command.ReservationCode);

            if (!reservations.Any())
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Reservation not found");

            var reserve = reservations.FirstOrDefault();

            reserve?.Cancel();

            room?.AddReservation(reserve);

            _roomRepository.Update(room);

            _roomRepository.SaveChanges();
        }
    }
}
