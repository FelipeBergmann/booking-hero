using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Infra.DataBase.Mappings.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHero.Booking.Infra.DataBase.Mappings
{
    internal class RoomMapping : EntityMapping<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.ToTable("Rooms");

            builder.Property(x => x.Number).HasMaxLength(6).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();

            builder.HasIndex(x => x.Name);

            builder.HasMany(x => x.Reservations)
                   .WithOne(x => x.Room);

        }
    }
}
