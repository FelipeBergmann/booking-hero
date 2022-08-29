namespace BookingHero.UseCase.Faults
{
    public class UseCaseError
    {
        public UseCaseError(UseCaseErrorType code, string description)
        {
            Code = code;
            Description = description;
        }

        public UseCaseErrorType Code { get; set; }
        public string Description { get; set; }
    }
}
