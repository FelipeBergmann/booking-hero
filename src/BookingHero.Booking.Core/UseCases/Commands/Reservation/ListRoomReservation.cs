namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    /// <summary>
    /// Command to list room's reservations
    /// </summary>
    public class ListRoomReservation
    {
        public ListRoomReservation(Guid roomId, DateOnly checkIn)
        {
            RoomId = roomId;
            CheckIn = checkIn;
        }

        /// <summary>
        /// Room's identifier
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Check in date
        /// </summary>
        public DateOnly CheckIn { get; set; }
    }
}
