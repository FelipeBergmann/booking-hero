namespace BookingHero.Booking.Core.Entities
{
    /// <summary>
    /// Represents hotel rooms
    /// </summary>
    public class Room : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }
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
