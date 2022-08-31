namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    /// <summary>
    /// Command to get a reservation
    /// </summary>
    public class GetReservationCommand
    {
        /// <summary>
        /// Reservation identifier
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Reservation code
        /// </summary>
        public string? Code { get; set; }
    }
}
