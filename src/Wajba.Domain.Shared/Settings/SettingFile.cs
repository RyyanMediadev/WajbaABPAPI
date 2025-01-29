using System;
using System.Globalization;

namespace Wajba.Settings;
public static class SettingFile
{
    public static DateTime? TryParseDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }
        DateTime date;
        if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            return date;
        }
        else
        {
            throw new ArgumentException("Invalid date format"); // Or return error message
        }
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
        else
        {
            // Handle parsing error (e.g., log error, return appropriate response)
            throw new ArgumentException("Invalid time format"); // Or return error message
        }
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