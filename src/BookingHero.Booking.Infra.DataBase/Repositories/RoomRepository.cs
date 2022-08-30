using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Infra.DataBase.DataAccess;
using BookingHero.Booking.Infra.DataBase.Repositories.Common;
using BookingHero.Booking.Core.Utils.Extensions;

namespace BookingHero.Booking.Infra.DataBase.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(BookingContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Reservation>> FindRoomReservationForCheckInDate(Guid roomId, DateOnly checkInDate)
        {
            var room = await base.GetByIdAsync(roomId);

            var result = _dbContext.Entry(room)
                                .Collection(r => r.Reservations)
                                .Query()
                                .Where(r => r.Status == ReservationStatus.Confirmed
                                        && (r.CheckIn <= checkInDate && r.CheckOut >= checkInDate))
                                .OrderBy(x => x.CreatedOn);
            return result;
        }

        public async Task<IEnumerable<Reservation>> FindReservation(Guid roomId, Guid? reservationId, string? reservationCode)
        {
            var room = await base.GetByIdAsync(roomId);

            var result = _dbContext.Entry(room)
                                .Collection(r => r.Reservations)
                                .Query()
                                .Where(r => (reservationId.IsEmpty() || r.Id == reservationId)
                                        && (string.IsNullOrEmpty(reservationCode) || r.Code == reservationCode));
            return result;
        }

        public async Task<Room> FindByNumber(string number)
        {
            var result = await Task.Run(() => SetEntity().Where(room => room.Number == number));

            return result.FirstOrDefault();
        }
    }
}
