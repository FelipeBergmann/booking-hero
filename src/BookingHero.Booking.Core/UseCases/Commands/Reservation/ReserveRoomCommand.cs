﻿namespace BookingHero.Booking.Core.UseCases.Commands.Reservation
{
    /// <summary>
    /// Command to book a room
    /// </summary>
    public class ReserveRoomCommand
    {
        public ReserveRoomCommand(Guid roomId, DateOnly checkIn, DateOnly checkOut, string customerEmail)
        {
            RoomId = roomId;
            CheckIn = checkIn;
            CheckOut = checkOut;
            CustomerEmail = customerEmail;
        }

        public Guid RoomId { get; private set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public string CustomerEmail { get; set; }
    }
}
