namespace BookingHero.Booking.Core.UseCases.Dto
{
    public class RoomAvailabilityDto
    {
        /// <summary>
        /// Room details
        /// </summary>
        public RoomDto Room { get; set; }
        /// <summary>
        /// Check in date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Room availability
        /// </summary>
        public RoomAvailabilityStatus Status { get; set; }

    }
}
