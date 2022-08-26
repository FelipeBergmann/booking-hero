using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Infra.DataBase.Mappings.Internal;
using BookingHero.Booking.Infra.DataBase.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHero.Booking.Infra.DataBase.Mappings
{
    internal class ReservationMapping : EntityMapping<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            builder.ToTable("Reservations");

            builder.Property(x => x.CheckIn).IsRequired().HasConversion<DateOnlyConverter, DateOnlyComparer>();
            builder.Property(x => x.CheckOut).IsRequired().HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Reservations);
        }
    }
}
