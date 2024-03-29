﻿using BookingHero.Booking.Core.UseCases.Reservation;
using BookingHero.Booking.Core.UseCases.Room;
using BookingHero.Booking.Core.UseCases.Room.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BookingHero.Booking.Core.UseCases.Extensions
{
    //Ideally this extension should stay in the infrastructure layer. To simplify this project it will stay on core layer temporarily
    public static class UseCaseDependencyInjectionExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateRoomUseCase, CreateRoomUseCase>();
            services.AddTransient<IReserveRoomUseCase, ReserveRoomUseCase>();
            services.AddTransient<ICancelReservationUseCase, CancelReservationUseCase>();
            services.AddTransient<IGetReservationUseCase, GetReservationUseCase>();
            services.AddTransient<IChangeReservationUseCase, ChangeReservationUseCase>();
            services.AddTransient<IListRoomReservationUseCase, ListRoomReservationUseCase>();
            services.AddTransient<IGetRoomUseCase, GetRoomUseCase>();
            services.AddTransient<IListRoomUseCase, ListRoomUseCase>();
            services.AddTransient<IRoomReservationAvailability, RoomReservationAvailability>();

            services.AddTransient<CreateRoomValidator>();
            services.AddTransient<GetRoomValidator>();
            services.AddTransient<RoomReservationValidator>();
            services.AddTransient<CancelReservationValidator>();
            services.AddTransient<ChangeReservationValidator>();
            services.AddTransient<ListRoomReservationValidator>();
            services.AddTransient<RoomReservationAvailabilityValidator>();

            return services;
        }
    }
}
