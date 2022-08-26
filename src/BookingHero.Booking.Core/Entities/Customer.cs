namespace BookingHero.Booking.Core.Entities
{
    /// <summary>
    /// Represents the person ordering reservations
    /// </summary>
    public class Customer : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Given name
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Customer's bookings <see cref="Reservation"/>
        /// </summary>
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
