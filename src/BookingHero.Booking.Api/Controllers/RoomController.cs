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
    }
}
