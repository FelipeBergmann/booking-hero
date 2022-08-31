using BookingHero.Booking.Core.UseCases.Commands.Reservation;
using BookingHero.Booking.Core.UseCases.Commands.Room;
using BookingHero.Booking.Core.UseCases.Dto;
using BookingHero.Booking.Core.UseCases.Reservation;
using BookingHero.Booking.Core.UseCases.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingHero.Booking.Api.Controllers
{
    [ApiController, Route("api/room"), Produces("application/json")]
    [AllowAnonymous]
    public class RoomController : ApiBaseController
    {
        /// <summary>
        /// Gets a room
        /// </summary>
        /// <param name="roomId">Target room identifier</param>
        /// <param name="useCase"></param>
        /// <returns>Found room</returns>
        [HttpGet("{roomId:guid}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRoom([FromRoute] Guid roomId, [FromServices] IGetRoomUseCase useCase)
        {
            await useCase.Resolve(new GetRoomCommand(roomId));

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Creates a room
        /// </summary>
        /// <param name="command"></param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoom()
        {
            return Ok();
        }


        /// <summary>
        /// List all rooms
        /// </summary>
        /// <param name="command"></param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListRooms([FromQuery] ListRoomCommand command, [FromServices] IListRoomUseCase useCase)
        {
            await useCase.Resolve(command);

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Gets the reservation by its id
        /// </summary>
        /// <param name="reservationId">reservation identifier</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("{roomId:guid}/reservation/{reservationId:guid}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
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
        /// <param name="command">payload</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpPost("{roomId:guid}/reservation")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RoomReservation([FromBody] ReserveRoomCommand command, [FromServices] IReserveRoomUseCase useCase)
        {
            await useCase.Resolve(command);

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Change a booking
        /// </summary>
        /// <param name="command">payload</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpPut("{roomId:guid}/reservation")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeRoomReservation([FromBody] ChangeReservationCommand command, [FromServices] IChangeReservationUseCase useCase)
        {
            await useCase.Resolve(command);

            return ResolveResult(useCase);
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="command">payload</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpDelete("{roomId:guid}/reservation")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelRoomReservation([FromBody] CancelReservationCommand command, [FromServices] ICancelReservationUseCase useCase)
        {
            await useCase.Resolve(command);

            return ResolveResult(useCase);
        }
    }
}
