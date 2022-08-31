using BookingHero.Booking.Core.Entities.Common;

namespace BookingHero.Booking.Core.Entities
{
    /// <summary>
    /// Represents hotel rooms
    /// </summary>
    public class Room : IEntity, IAggregrateRoot
    {
        protected Room()
        {

        }

        public Room(Guid id, string name, string number)
        {
            Id = id;
            Name = name;
            Number = number;
        }

        /// <inheritdoc/>
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
        /// Represents room's bookings
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; }

        public Room AddReservation(Reservation reservation)
        {
            if (Reservations == null)
                Reservations = new List<Reservation>();

            Reservations.Add(reservation);

            return this;
        }

        public Room RemoveReservation(Reservation reservation)
        {
            if (Reservations == null)
                return this;

            Reservations.Remove(reservation);

            return this;
        }
    }
}
