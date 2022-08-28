using Microsoft.EntityFrameworkCore;

namespace BookingHero.Booking.Infra.DataBase.Mappings.Extension
{
    internal static class MappingExtension
    {
        internal static ModelBuilder ApplyMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomMapping());
            modelBuilder.ApplyConfiguration(new ReservationMapping());    

            return modelBuilder;
        }
    }
}
