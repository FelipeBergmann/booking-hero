namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    /// <summary>
    /// Command to get a reservation
    /// </summary>
    public class GetReservationCommand
    {
        public GetReservationCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Reservation identifier
        /// </summary>
        public Guid Id { get; private set; }
    }
}
