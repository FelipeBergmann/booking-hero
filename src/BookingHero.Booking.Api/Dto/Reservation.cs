using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Core.UseCases.Dto;

namespace BookingHero.Booking.Api.Dto
{
    public class Reservation
    {
        /// <summary>
        /// Reservation identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Reservation code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Customer e-mail
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Check In date
        /// </summary>
        public DateOnly CheckIn { get; set; }

        /// <summary>
        /// Check Out date
        /// </summary>
        public DateOnly CheckOut { get; set; }

        /// <summary>
        /// Represents the status of the reservation <see cref="ReservationStatus"/>
        /// </summary>
        public ReservationStatus Status { get; set; }

        public static implicit operator Reservation(ReservationDto dto) => new()
        {
            Id = dto.Id,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut,
            Status = dto.Status,
            CustomerEmail = dto.CustomerEmail
        };
    }
}
