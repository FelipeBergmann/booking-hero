namespace BookingHero.Booking.Core.UseCases.Commands.Room
{
    /// <summary>
    /// Command for get a room
    /// </summary>
    public class GetRoomCommand
    {
        public GetRoomCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Entity identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
