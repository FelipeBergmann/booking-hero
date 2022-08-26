namespace BookingHero.Booking.Core.Entities
{
    /// <summary>
    /// Represents reservations of rooms
    /// </summary>
    public class Reservation : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Reserved for <see cref="Customer"/>
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Check In date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Check Out date
        /// </summary>
        public DateOnly CheckOut { get; set; }
    }
}
