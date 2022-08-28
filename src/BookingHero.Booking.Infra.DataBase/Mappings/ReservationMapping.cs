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
            builder.Property(x => x.CustomerEmail).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(8);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("getdate()").HasPrecision(7);

            builder.HasOne(x => x.Room)
                   .WithMany(x => x.Reservations)
                   .HasForeignKey("RoomId");
        }
    }
}
