namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    public class CancelReservationCommand
    {
        public CancelReservationCommand(Guid roomId, string reservationCode)
        {
            RoomId = roomId;
            ReservationCode = reservationCode;
        }

        /// <summary>
        /// Room's identifier
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Reservation code
        /// </summary>
        public string ReservationCode { get; set; }
    }
}
