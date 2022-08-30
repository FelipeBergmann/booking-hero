using BookingHero.Booking.Core.Utils.Extensions;

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
        public DateTime CreatedOn { get; private set; }

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

        /// <summary>
        /// Generates a 8 digit reservation code to simplify for users
        /// </summary>
        /// <returns></returns>
        public static string GenerateReservationCode() => Guid.NewGuid().ToString()[..8];

        /// <summary>
        /// Checks if the reservation time (between check in and check out) is allowed
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public static bool NotAllowedStayPeriod(DateOnly checkIn, DateOnly checkOut) => checkOut.CompareDifferenceInDays(checkIn) >= 3;

        /// <summary>
        /// Checks if the reservation anticipation is allowed
        /// </summary>
        /// <param name="checkIn"></param>
        /// <returns></returns>
        public static bool NotAllowedAdvanceReservation(DateOnly checkIn) => checkIn.CompareDifferenceInDays(DateTime.Today) > 30;

        /// <summary>
        /// Checks if the check in date is greater than the current date
        /// </summary>
        /// <param name="checkIn"></param>
        /// <returns></returns>
        public static bool CheckInDateNotAllowed(DateOnly checkIn) => checkIn.CompareTo(DateOnly.FromDateTime(DateTime.Today)) <= 0;
    }
}
