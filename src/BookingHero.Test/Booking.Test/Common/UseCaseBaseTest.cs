using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Room;
using BookingHero.Booking.Infra.DataBase.DataAccess;
using BookingHero.Booking.Infra.DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Booking.UnitTest.Common
{
    public abstract class UseCaseBaseTest
    {
        private readonly IServiceCollection _services;
        protected readonly IServiceProvider _serviceProvider;

        public UseCaseBaseTest()
        {
            _services = new ServiceCollection();

            _services.AddLogging();

            using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = logFactory.CreateLogger("Test");
            
            _services.AddSingleton<ILogger>(x => logger);

            // Add services to the container.
            _services.AddDbContext<BookingContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelHero;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("BookingHero.Booking.Infra.DataBase"));
                options.LogTo(Console.WriteLine);
            });

            _services.AddTransient<IRoomRepository, RoomRepository>();

            _services.AddTransient<IGetRoomUseCase, GetRoomUseCase>();
            _services.AddTransient<IListRoomUseCase, ListRoomUseCase>();

            _serviceProvider = _services.BuildServiceProvider();
        }
    }
}
