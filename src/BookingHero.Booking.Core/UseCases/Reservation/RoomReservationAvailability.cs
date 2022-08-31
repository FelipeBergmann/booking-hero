using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class RoomReservationAvailability : UseCaseBase<RoomReservationAvailabilityCommand, RoomAvailabilityDto>, IRoomReservationAvailability
    {
        private readonly IRoomRepository _roomRepository;
        public RoomReservationAvailability(ILogger<RoomReservationAvailability> logger, IRoomRepository roomRepository, RoomReservationAvailabilityValidator validator) : base(logger, validator)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<RoomAvailabilityDto> Execute(RoomReservationAvailabilityCommand command)
        {
            var room = await _roomRepository.GetByIdAsync(command.RoomId);

            if (room == null)
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Room not found");

            var reservations = await _roomRepository.FindRoomReservationForCheckInDate(room.Id, command.CheckIn);

            return new RoomAvailabilityDto()
            {
                Room = room,
                CheckIn = command.CheckIn,
                Status = reservations.Any() ? RoomAvailabilityStatus.Taken : RoomAvailabilityStatus.Free
            };
        }
    }
}
