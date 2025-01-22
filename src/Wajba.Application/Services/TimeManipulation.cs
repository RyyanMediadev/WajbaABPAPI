global using System.Globalization;

namespace Wajba.Services;

public static class TimeManipulation
{
    public static DateTime? TryParseDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }
        DateTime date;
        if (DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            return date;
        }
        return null;
    }

    public static DateTime? TryParseTime(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return null;
        }
        DateTime time;
        if (DateTime.TryParseExact(timeString, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
        {
            return time;
        }
        return null;
    }
    public static string FormatTime(DateTime? time)
    {
        return time?.ToString(@"hh\:mm tt", CultureInfo.InvariantCulture);
    }
    public static string FormatDate(DateTime? date)
    {
        return date?.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
    }
}
