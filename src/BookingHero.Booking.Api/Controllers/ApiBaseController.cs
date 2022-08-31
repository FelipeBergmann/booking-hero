using BookingHero.UseCase;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingHero.Booking.Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult ResolveResult<TCommand, TOut>(IUseCase<TCommand, TOut> useCase)
        {
            if (useCase.IsFaulted)
            {
                var firstError = useCase.GetErrors().FirstOrDefault();
                var serializedErrors = JsonConvert.SerializeObject(useCase.GetErrors());
                switch (firstError?.Code)
                {
                    case UseCase.Faults.UseCaseErrorType.InternalError:
                    case UseCase.Faults.UseCaseErrorType.Unknown:
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails()
                        {
                            Title = "Internal Error",
                            Detail = serializedErrors
                        });
                    case UseCase.Faults.UseCaseErrorType.BadRequest:
                        return Problem(title: "Bad Request", detail: serializedErrors, statusCode: StatusCodes.Status400BadRequest);
                }
            }

            return Ok(useCase.UseCaseResult);
        }

        protected IActionResult ResolveResult<TCommand>(IUseCase<TCommand> useCase)
        {
            if (useCase.IsFaulted)
            {
                var firstError = useCase.GetErrors().FirstOrDefault();
                var serializedErrors = JsonConvert.SerializeObject(useCase.GetErrors());
                switch (firstError?.Code)
                {
                    case UseCase.Faults.UseCaseErrorType.InternalError:
                    case UseCase.Faults.UseCaseErrorType.Unknown:
                    default:
                        return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails()
                        {
                            Title = "Internal Error",
                            Detail = serializedErrors
                        });
                    case UseCase.Faults.UseCaseErrorType.BadRequest:
                        return Problem(title: "Bad Request", detail: serializedErrors, statusCode: StatusCodes.Status400BadRequest);
                }
            }

            return Ok();
        }
    }
}
