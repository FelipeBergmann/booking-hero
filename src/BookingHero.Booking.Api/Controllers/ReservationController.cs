using BookingHero.Booking.Api.Payloads;
using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Reservation;
using BookingHero.Booking.Core.UseCases.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingHero.Booking.Api.Controllers
{
    
    [ApiVersion("1.0")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]"), Produces("application/json")]
    [AllowAnonymous]
    public class ReservationController : ApiBaseController
    {
         /// <summary>
        /// Verifies the availability of the room for the provided check in date
        /// </summary>
        /// <param name="roomId">Room's identifier</param>
        /// <param name="checkIn">Desired check in date</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("{roomId:guid}/availability")]
        [ProducesResponseType(typeof(IEnumerable<Dto.RoomAvailability>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoomAvailability([FromRoute] Guid roomId, [FromQuery(Name = "checkIn")]DateTime checkIn, [FromServices] IRoomReservationAvailability useCase)
        {
            await useCase.Resolve(new RoomReservationAvailabilityCommand(roomId, DateOnly.FromDateTime(checkIn)));

            return ResolveResult(useCase);
        }
       
        /// <summary>
        /// Gets the reservation by its id
        /// </summary>
        /// <param name="reservationId">reservation identifier</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("{roomId:guid}/reservation/{reservationId:guid}")]
        [ProducesResponseType(typeof(Dto.Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReservation([FromRoute] Guid reservationId, [FromServices] IGetReservationUseCase useCase)
        {
            await useCase.Resolve(new GetReservationCommand()
            {
                Id = reservationId
            });

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Gets the reservation by its code
        /// </summary>
        /// <param name="reservationCode">Reservation code</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("{roomId:guid}/reservation/code/{reservationCode}")]
        [ProducesResponseType(typeof(Dto.Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReservation([FromRoute] string reservationCode, [FromServices] IGetReservationUseCase useCase)
        {
            await useCase.Resolve(new GetReservationCommand()
            {
                Code = reservationCode
            });

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Booking a room
        /// </summary>
        /// <param name="roomId">Room's identifier</param>
        /// <param name="command">payload</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpPost("{roomId:guid}/reservation")]
        [ProducesResponseType(typeof(Dto.Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoomReservation([FromRoute] Guid roomId, [FromBody] RoomReservation command, [FromServices] IReserveRoomUseCase useCase)
        {
            await useCase.Resolve(new ReserveRoomCommand(roomId, command.CheckIn, command.CheckOut, command.CustomerEmail));

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Change a booking
        /// </summary>
        /// <param name="roomId">Room's identifier</param>
        /// <param name="reservationCode">Reservation code</param>
        /// <param name="command">payload</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpPut("{roomId:guid}/reservation/{reservationCode}")]
        [ProducesResponseType(typeof(Dto.Reservation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeRoomReservation([FromRoute] Guid roomId, [FromRoute] string reservationCode, [FromBody] ChangeRoomReservation command, [FromServices] IChangeReservationUseCase useCase)
        {
            await useCase.Resolve(new ChangeReservationCommand(roomId, reservationCode)
            {
                CheckIn = command.CheckIn,
                CheckOut = command.CheckOut
            });

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="roomId">Room's id</param>
        /// <param name="reservationCode">Target reservation code</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpDelete("{roomId:guid}/reservation/{reservationCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelRoomReservation([FromRoute] Guid roomId, [FromRoute] string reservationCode, [FromServices] ICancelReservationUseCase useCase)
        {
            await useCase.Resolve(new CancelReservationCommand(roomId, reservationCode));

            return ResolveResult(useCase);
        }
    }
}
