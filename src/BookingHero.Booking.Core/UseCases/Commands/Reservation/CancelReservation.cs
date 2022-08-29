namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    public class CancelReservation
    {
        public CancelReservation(Guid roomId, string reservationCode)
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
