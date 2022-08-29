namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    public class ConfirmReservation
    {
        public ConfirmReservation(Guid roomId, string reservationCode)
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
