using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Infra.DataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingHero.Booking.Infra.DataBase.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRoomRepository, RoomRepository>();
            return services;
        }
    }
}
