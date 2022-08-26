using BookingHero.Booking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using BookingHero.Booking.Infra.DataBase.Mappings.Extension;

namespace BookingHero.Booking.Infra.DataBase.DataAccess
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyMappings();
        }
    }
}
