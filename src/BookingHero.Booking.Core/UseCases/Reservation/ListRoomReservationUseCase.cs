using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class ListRoomReservationUseCase : UseCaseBase<ListRoomReservationCommand, RoomReservationDto>, IListRoomReservationUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public ListRoomReservationUseCase(ILogger<ListRoomReservationUseCase> logger, IRoomRepository roomRepository, ListRoomReservationValidator validator) : base(logger, validator)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<RoomReservationDto> Execute(ListRoomReservationCommand command)
        {
            var reservation = await _roomRepository.FindRoomReservationForCheckInDate(command.RoomId, command.CheckIn);

            RoomReservationDto roomReservation = new RoomReservationDto();

            if (!reservation.Any())
                return roomReservation;

            roomReservation = (RoomReservationDto)reservation.FirstOrDefault()!.Room;

            roomReservation.Reservations = reservation.Select(r => (ReservationDto)r).ToList();

            return roomReservation;
        }
    }
}
