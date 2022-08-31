namespace BookingHero.Booking.Api.Payloads
{
    /// <summary>
    /// Payload for changing a booking
    /// </summary>
    public class ChangeRoomReservation
    {
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
