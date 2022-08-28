using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Core.Repositories.Common;

namespace BookingHero.Booking.Core.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> FindByNumber(string number);
        Task<IEnumerable<Reservation>> FindConfirmedBookings(Guid roomId, DateOnly checkInDate);
    }
}
