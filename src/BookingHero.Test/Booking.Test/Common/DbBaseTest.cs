using BookingHero.Booking.Infra.DataBase.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Booking.UnitTest.Common
{
    internal abstract class DbBaseTest
    {
        private readonly IServiceCollection _services;
        protected readonly IServiceProvider _serviceProvider;

        public DbBaseTest()
        {
            _services = new ServiceCollection();

            // Add services to the container.
            _services.AddDbContext<BookingContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelHero;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("BookingHero.Booking.Infra.DataBase"));
            });

            _serviceProvider = _services.BuildServiceProvider();
        }
    }
}
