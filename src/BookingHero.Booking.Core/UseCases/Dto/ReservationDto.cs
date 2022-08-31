using BookingHero.Booking.Core.Entities;

namespace BookingHero.Booking.Core.UseCases.Dto
{
    public class ReservationDto
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

        public static implicit operator ReservationDto(Entities.Reservation reservation) => new()
        {
            Id = reservation.Id,
            Code = reservation.Code,
            CustomerEmail = reservation.CustomerEmail,
            CheckIn = reservation.CheckIn,
            CheckOut = reservation.CheckOut,
            Status = reservation.Status
        };
    }
}
