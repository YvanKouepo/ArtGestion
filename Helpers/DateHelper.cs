namespace ArtGestion.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetCameroonTime()
        {
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            }
            catch
            {
                return DateTime.UtcNow.AddHours(1);
            }
        }
    }
}