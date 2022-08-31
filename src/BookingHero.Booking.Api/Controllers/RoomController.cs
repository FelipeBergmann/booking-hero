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
    public class RoomController : ApiBaseController
    {
        /// <summary>
        /// Gets a room
        /// </summary>
        /// <param name="roomId">Target room identifier</param>
        /// <param name="useCase"></param>
        /// <returns>Found room</returns>
        [HttpGet("{roomId:guid}")]
        [ProducesResponseType(typeof(Dto.Room), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Dto.Room), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoom command, [FromServices]ICreateRoomUseCase useCase)
        {
            await useCase.Resolve(new CreateRoomCommand(command.Name, command.Number));

            return ResolveResult(useCase);
        }


        /// <summary>
        /// List all rooms
        /// </summary>
        /// <param name="number">Room's number</param>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Dto.Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListRooms([FromQuery(Name = "number")] string? number, [FromServices] IListRoomUseCase useCase)
        {
            await useCase.Resolve(new ListRoomCommand()
            {
                Number = number
            });

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
