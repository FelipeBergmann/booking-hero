namespace BookingHero.Booking.Core.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// Entity's identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
