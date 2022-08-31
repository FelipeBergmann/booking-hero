namespace BookingHero.Booking.Core.UseCases.Commands.Room
{
    /// <summary>
    /// Command for get a room
    /// </summary>
    public class CreateRoomCommand
    {
        public CreateRoomCommand(string name, string number)
        {
            Name = name;
            Number = number;
        }

        /// <summary>
        /// Room's name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Room's number
        /// </summary>
        public string Number { get; private set; }
    }
}
