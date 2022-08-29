namespace BookingHero.Booking.Core.Entities
{
    /// <summary>
    /// Represents reservations of rooms
    /// </summary>
    public class Reservation : IEntity
    {
        protected Reservation()
        {

        }

        public Reservation(Guid id, string customerEmail, Room room, DateOnly checkIn, DateOnly checkOut, ReservationStatus status, string code)
        {
            Id = id;
            CustomerEmail = customerEmail;
            Room = room;
            CheckIn = checkIn;
            CheckOut = checkOut;
            Status = status;
            Code = code;
        }

        /// <inheritdoc/>
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
        /// Represents the room
        /// </summary>
        public Room Room { get; set; }

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

        /// <summary>
        /// When the reservation was created
        /// </summary>
        public DateTime CreatedAOn { get; private set; }

        /// <summary>
        /// Confirms the reservation
        /// </summary>
        /// <returns></returns>
        public Reservation Confirm()
        {
            if (Status == ReservationStatus.NotConfirmed)
                Status = ReservationStatus.Confirmed;

            return this;
        }

        /// <summary>
        /// Cancels the reservation
        /// </summary>
        /// <returns></returns>
        public Reservation Cancel()
        {
            if (Status != ReservationStatus.Canceled)
                Status = ReservationStatus.Canceled;
            return this;
        }
    }
}
