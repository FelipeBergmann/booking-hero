namespace BookingHero.Booking.Api.Payloads
{
    /// <summary>
    /// Payload to create a new room
    /// </summary>
    public class CreateRoom
    {
        /// <summary>
        /// Room's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Room's number
        /// </summary>
        public string Number { get; set; }
    }
}
