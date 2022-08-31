using BookingHero.Booking.Core.UseCases.Dto;

namespace BookingHero.Booking.Api.Dto
{
    /// <summary>
    /// Represents the room availability
    /// </summary>
    public class RoomAvailability
    {
        /// <summary>
        /// Room details
        /// </summary>
        public RoomDto Room { get; set; }
        /// <summary>
        /// Check in date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Room availability
        /// </summary>
        public RoomAvailabilityStatus Status { get; set; }

        public static implicit operator RoomAvailability(RoomAvailabilityDto dto) => new()
        {
            Status = dto.Status,
            CheckIn = dto.CheckIn,
            Room = dto.Room
        };
    }
}
