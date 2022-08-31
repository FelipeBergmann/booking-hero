using BookingHero.Booking.Core.UseCases.Dto;

namespace BookingHero.Booking.Api.Dto
{
    public class Room
    {
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

        public static implicit operator Room(RoomDto dto) => new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Number = dto.Number
        };
    }
}
