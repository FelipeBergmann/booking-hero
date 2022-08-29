namespace BookingHero.Booking.Core.UseCases.Dto
{
    public class RoomDto
    {
        public RoomDto(Guid id, string name, string number)
        {
            Id = id;
            Name = name;
            Number = number;
        }

        /// <summary>
        /// Room's identifier
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Room's name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Room's number
        /// </summary>
        public string Number { get; private set; }

        public static implicit operator RoomDto(Entities.Room entity) => new(entity.Id, entity.Name, entity.Number);
    }
}
