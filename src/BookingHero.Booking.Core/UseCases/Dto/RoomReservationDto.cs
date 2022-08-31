namespace BookingHero.Booking.Core.UseCases.Dto
{
    public class RoomReservationDto
    {
        /// <summary>
        /// Room's identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Room's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Room's number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Room's reservation
        /// </summary>
        public ICollection<ReservationDto> Reservations { get; set; }

        public static explicit operator RoomReservationDto(Entities.Room room) => new ()
        {
            Id = room.Id,
            Name = room.Name,
            Number = room.Number,
            Reservations = new List<ReservationDto>()
        };
    }
}
