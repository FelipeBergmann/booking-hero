using BookingHero.UseCase.Execution;
using BookingHero.UseCase.Faults;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookingHero.UseCase
{
    public abstract class UseCaseBase<TCommand> : IUseCase<TCommand>
    {
        protected readonly ILogger _logger;
        protected readonly ExecutionLogList _executionLog = new ExecutionLogList();
        protected readonly List<UseCaseError> _useCaseError = new List<UseCaseError>();

        protected UseCaseBase(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsFaulted { get => _useCaseError.Any(); }

        public async Task Resolve(TCommand command)
        {
            _executionLog.AddInfo($"Starting to resolve { typeof(TCommand) }")
                         .AddInfo($"Received command: {JsonConvert.SerializeObject(command)}");

            //todo: implement validation
            try
            {
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

        public void Fault(UseCaseErrorType code, string message)
        {
            throw new UseCaseException(code, message);
        }
    }

    public abstract class UseCaseBase<TCommand, TOut> : UseCaseBase<TCommand>, IUseCase<TCommand, TOut>
    {
        protected UseCaseBase(ILogger logger) : base(logger) { }

        public TOut UseCaseResult { get; private set; }

        protected override abstract Task<TOut> Execute(TCommand command);

        public async Task Resolve(TCommand command)
        {
            _executionLog.AddInfo($"Starting to resolve { typeof(TCommand) }")
                         .AddInfo($"Received command: {JsonConvert.SerializeObject(command)}");

            //todo: implement validation
            try
            {
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
