namespace BookingHero.Booking.Core.Utils.Extensions
{
    public static class DateTimeExtension
    {
        public static double CompareDifferenceInDays(this DateTime first, DateTime second) => (first - second).TotalDays;
        public static double CompareDifferenceInDays(this DateOnly first, DateOnly second) => (first.ToDateTimeInvariant() - second.ToDateTimeInvariant()).TotalDays;
        public static double CompareDifferenceInDays(this DateTime first, DateOnly second) => (first - second.ToDateTimeInvariant()).TotalDays;
        public static double CompareDifferenceInDays(this DateOnly first, DateTime second) => (first.ToDateTimeInvariant() - second).TotalDays;
        public static DateTime ToDateTimeInvariant(this DateOnly date) => date.ToDateTime(TimeOnly.Parse("00:00:00"));
    }
}
