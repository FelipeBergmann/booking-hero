namespace BookingHero.Booking.Core.Utils.Extensions
{
    public static class GuidExtension
    {
        public static bool IsEmpty(this Guid? value)
        {
            return value == null || Guid.Equals(value, Guid.Empty);
        }
    }
}
