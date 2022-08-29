﻿using BookingHero.Booking.Core.Repositories;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.UseCase;
using BookingHero.UseCase.Faults;
using Microsoft.Extensions.Logging;

namespace BookingHero.Booking.Core.UseCases.Room
{
    public class GetRoomUseCase : UseCaseBase<GetRoomCommand, RoomDto>, IGetRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public GetRoomUseCase(ILogger logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        protected override async Task<RoomDto> Execute(GetRoomCommand command)
        {
            var room = await _roomRepository.GetByIdAsync(command.Id);

            if (room == null)
                Fault(UseCaseErrorType.BadRequest, "Room not found");

            return room!;
        }
    }   
}
