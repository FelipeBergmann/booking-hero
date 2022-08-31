namespace BookingHero.Booking.Api.Payloads
{
    /// <summary>
    /// Payload for booking a room
    /// </summary>
    public class RoomReservation
    {
        /// <summary>
        /// Check in date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Check out date
        /// </summary>
        public DateOnly CheckOut { get; set; }

        /// <summary>
        /// Customer's e-mail
        /// </summary>
        public string CustomerEmail { get; set; }
    }
}
