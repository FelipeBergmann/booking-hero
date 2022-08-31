using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Reservation
{
    public class ReserveRoomUseCase : UseCaseBase<ReserveRoomCommand, ReservationDto>, IReserveRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public ReserveRoomUseCase(ILogger logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<ReservationDto> Execute(ReserveRoomCommand command)
        {
            var room = await _roomRepository.GetByIdAsync(command.RoomId);

            if (room == null)
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Room not found");

            if (Entities.Reservation.CheckInDateNotAllowed(command.CheckIn))
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Reservations are allowed starting from tomorrow only");

            if (Entities.Reservation.NotAllowedStayPeriod(command.CheckIn, command.CheckOut))
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Unfortonately, you can't reserve for more than three days");

            if(Entities.Reservation.NotAllowedAdvanceReservation(command.CheckIn))
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "Unfortonately, you can't reserve for more thirty days in advance");

            var reservations = await _roomRepository.FindRoomReservationForCheckInDate(command.RoomId, command.CheckIn);

            if (reservations.Any())
                Fault(UseCase.Faults.UseCaseErrorType.BadRequest, "This room is already reserved for the provided check in date");

            var reservationEntity = new Entities.Reservation(Guid.NewGuid(),
                                                             command.CustomerEmail,
                                                             room,
                                                             command.CheckIn,
                                                             command.CheckOut,
                                                             Entities.ReservationStatus.Confirmed,
                                                             Entities.Reservation.GenerateReservationCode());

            room?.AddReservation(reservationEntity);

            _roomRepository.Update(room);

            _roomRepository.SaveChanges();

            reservations = await _roomRepository.FindRoomReservationForCheckInDate(command.RoomId, command.CheckIn);
            
            if(reservations.Count() > 1 && reservations.First().Id != reservationEntity.Id)
            {
                //todo: cancel

                Fault(UseCase.Faults.UseCaseErrorType.Unknown, "We apologize. Unfortonately, was not possible to confirm your reservation. The check in date was already booked.");
            }

            return reservationEntity;
        }
    }
}
