namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    /// <summary>
    /// Command to get a reservation
    /// </summary>
    public class GetReservationCodeCommand
    {
        public GetReservationCodeCommand(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Reservation code
        /// </summary>
        public string Code { get; private set; }
    }
}
