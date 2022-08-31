using BookingHero.UseCase.Execution;
using BookingHero.UseCase.Faults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookingHero.UseCase
{
    public abstract class UseCaseBase<TCommand> : IUseCase<TCommand>
    {
        protected readonly ILogger _logger;
        protected readonly ExecutionLogList _executionLog = new ExecutionLogList();
        protected readonly List<UseCaseError> _useCaseError = new List<UseCaseError>();
        protected readonly AbstractValidator<TCommand> _validator;

        protected UseCaseBase(ILogger logger, AbstractValidator<TCommand>? validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public bool IsFaulted { get => _useCaseError.Any(); }

        public async Task Resolve(TCommand command)
        {
            _executionLog.AddInfo($"Starting to resolve { typeof(TCommand) }")
                         .AddInfo($"Received command: {JsonConvert.SerializeObject(command)}");

            try
            {
                if (_validator != null)
                {
                    _executionLog.AddDebug("Initializing command validation");

                    await ValidateCommandInput(command, _validator);

                    _executionLog.AddDebug("Provided command is valid");
                }

                _executionLog.AddDebug("Initializing command execution");
                await Execute(command);
                _executionLog.AddInfo($"Resolved completed { typeof(TCommand) }");
            }
            catch (UseCaseException ucex)
            {
                _useCaseError.Add(new UseCaseError(ucex.Code, ucex.Message));
                _executionLog.AddError(ucex.Message);
            }
            catch (Exception ex)
            {
                _useCaseError.Add(new UseCaseError(UseCaseErrorType.InternalError, ex.Message));
                _executionLog.AddError(ex.Message);
            }
            finally
            {
                _logger.LogInformation(message: _executionLog.ToString());
            }
        }

        public ICollection<UseCaseError> GetErrors() => _useCaseError;

        protected abstract Task Execute(TCommand command);

        /// <summary>
        /// Performs a validation
        /// Calls Fault to stop the flow
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual async Task ValidateCommandInput(TCommand command, AbstractValidator<TCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                Fault(UseCaseErrorType.BadRequest, SerializeValdationErrors(validationResult.Errors));
        }

        public void Fault(UseCaseErrorType code, string message)
        {
            throw new UseCaseException(code, message);
        }

        protected string SerializeValdationErrors(List<ValidationFailure> errors)
        {
            return JsonConvert.SerializeObject(errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage }));
        }
    }

    public abstract class UseCaseBase<TCommand, TOut> : UseCaseBase<TCommand>, IUseCase<TCommand, TOut>
    {
        protected UseCaseBase(ILogger logger, AbstractValidator<TCommand>? validator = null) : base(logger, validator) { }

        public TOut UseCaseResult { get; private set; }

        protected override abstract Task<TOut> Execute(TCommand command);

        public async Task Resolve(TCommand command)
        {
            _executionLog.AddInfo($"Starting to resolve { typeof(TCommand) }")
                         .AddInfo($"Received command: {JsonConvert.SerializeObject(command)}");

            try
            {
                if (_validator != null)
                {
                    _executionLog.AddDebug("Initializing command validation");

                    await ValidateCommandInput(command, _validator);

                    _executionLog.AddDebug("Provided command is valid");
                }

                _executionLog.AddDebug("Initializing command execution");

                UseCaseResult = await Execute(command);

                _executionLog.AddInfo($"Resolved completed { typeof(TCommand) }");
            }
            catch (UseCaseException ucex)
            {
                _useCaseError.Add(new UseCaseError(ucex.Code, ucex.Message));
                _executionLog.AddError(ucex.Message);
            }
            catch (Exception ex)
            {
                _useCaseError.Add(new UseCaseError(UseCaseErrorType.InternalError, ex.Message));
                _executionLog.AddError(ex.Message);
            }
            finally
            {
                _logger.LogInformation(message: _executionLog.ToString());
            }
        }
    }
}
