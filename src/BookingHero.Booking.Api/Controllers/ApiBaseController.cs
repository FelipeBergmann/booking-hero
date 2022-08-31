using BookingHero.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace BookingHero.Booking.Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult ResolveResult<TCommand, TOut>(IUseCase<TCommand, TOut> useCase, string httpVerb = "Any")
        {
            if (useCase.IsFaulted)
            {
                var firstError = useCase.GetErrors().FirstOrDefault();
                var serializedErrors = string.Join(", ", useCase.GetErrors().Select(x => x.Description));
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

            if (httpVerb.Equals("post", StringComparison.InvariantCultureIgnoreCase))
                return StatusCode(StatusCodes.Status201Created, useCase.UseCaseResult);
            else
                return Ok(useCase.UseCaseResult);
        }

        protected IActionResult ResolveResult<TCommand>(IUseCase<TCommand> useCase, string httpVerb = "Any")
        {
            if (useCase.IsFaulted)
            {
                var firstError = useCase.GetErrors().FirstOrDefault();
                var serializedErrors = string.Join(", ", useCase.GetErrors().Select(x => x.Description));
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

            if (httpVerb.Equals("post", StringComparison.InvariantCultureIgnoreCase))
                return StatusCode(StatusCodes.Status201Created);
            else
                return Ok();
        }
    }
}
