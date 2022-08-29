using BookingHero.UseCase.Faults;

namespace BookingHero.UseCase
{
    public interface IUseCase<TCommand>
    {
        public Task Resolve(TCommand command);

        public void Fault(UseCaseErrorType code, string message);

        public bool IsFaulted { get; }

        public ICollection<UseCaseError> GetErrors();

    }

    public interface IUseCase<TCommand, TOut> : IUseCase<TCommand>
    {
        TOut UseCaseResult { get; }
    }
}
