using BookingHero.Booking.Core.Entities;
using BookingHero.Booking.Infra.DataBase.Mappings.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingHero.Booking.Infra.DataBase.Mappings
{
    internal class CustomerMapping : EntityMapping<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.ToTable("Customers");

            builder.Property(x => x.GivenName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired();

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.Customer);
        }
    }
}
