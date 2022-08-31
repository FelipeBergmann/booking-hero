namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    public class ChangeReservationCommand
    {
        public ChangeReservationCommand(Guid roomId, string reservationCode)
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

        /// <summary>
        /// Check in date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Check out date
        /// </summary>
        public DateOnly CheckOut { get; set; }
    }
}
