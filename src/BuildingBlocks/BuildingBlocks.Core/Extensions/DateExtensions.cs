namespace BuildingBlocks.Core.Extensions;

public static class DateExtensions
{
    // for modify date by timezone
    public static DateTime DateTimeNow => DateTime.Now.SetKindUtc();

    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Utc ? dateTime : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}