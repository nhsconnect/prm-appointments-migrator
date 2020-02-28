using System;

namespace GPConnectAdaptor.Slots
{
    public class DateTimeGenerator : IDateTimeGenerator
    {
        public string Generate(DateTime dateTime)
        {
            return (dateTime.ToString("s") + "+" + TimeZoneInfo.Local.BaseUtcOffset).Substring(0, 25);
        }

        public string GenerateDate(DateTime date)
        {
            return date.ToString("s").Substring(0,10);
        }
    }
}