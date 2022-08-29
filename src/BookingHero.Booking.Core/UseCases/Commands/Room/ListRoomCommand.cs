namespace BookingHero.Booking.Core.UseCases.Commands.Room
{
    /// <summary>
    /// Command for list rooms
    /// </summary>
    public class ListRoomCommand
    {
        /// <summary>
        /// Room's number
        /// </summary>
        public string? Number { get; set; }
    }
}
