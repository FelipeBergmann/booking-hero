using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Core.Repositories.Common;

namespace BookingHero.Booking.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        /// <summary>
        /// Finds a room by its number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<Room> FindByNumber(string number);

        /// <summary>
        /// Finds any confirmed reservation for provided check in date
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="checkInDate"></param>
        /// <returns></returns>
        Task<IEnumerable<Reservation>> FindRoomReservationForCheckInDate(Guid roomId, DateOnly checkInDate);

        /// <summary>
        /// Finds a reservation
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="reservationId"></param>
        /// <param name="reservationCode"></param>
        /// <returns></returns>
        Task<IEnumerable<Reservation>> FindReservation(Guid roomId, Guid? reservationId, string? reservationCode);

        /// <summary>
        /// Update reservation status
        /// </summary>
        /// <param name="reservationId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        Task UpdateReservationStatus(Guid reservationId, ReservationStatus newStatus);

        /// <summary>
        /// Finds a reservation by its identifier or code
        /// </summary>
        /// <param name="reservationId"></param>
        /// <param name="reservationCode"></param>
        /// <returns></returns>
        Task<Reservation> FindReservation(Guid? reservationId, string? reservationCode);
    }
}
