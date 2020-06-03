using System;

namespace UIAutomationFramework.Utils
{
    public static class DateTimeUtils
    {
        private const string DateFormat = "MM/dd/yyyy";
        private static long _lastUsed;

        public static string GetTodayDate()
        {
            return DateTime.Now.ToString(DateFormat);
        }

        public static DateTime GetRandomDate(int startYear, int endYear)
        {
            var rnd = new Random();
            return new DateTime(rnd.Next(startYear, endYear), rnd.Next(1, 12), rnd.Next(1, 28));
        }

        public static long GetTimeStamp()
        {
            return DateTime.Now.Ticks;
        }

        public static string GetUniqueStringFromTimestamp()
        {
            if (_lastUsed == GetTimeStamp()) return GetUniqueStringFromTimestamp();

            var timestamp = GetTimeStamp();
            _lastUsed = timestamp;
            return "" + timestamp;
        }

        public static string GetUniqueStringFromTimestamp(int length)
        {
            var timestamp = "" + GetUniqueStringFromTimestamp();
            return timestamp.Substring(timestamp.Length - length);
        }

        public static DateTime? GetDate(string date)
        {
            if (string.IsNullOrEmpty(date)) return null;

            return DateTime.Parse(date);
        }

        public static DateTime? GetDate(string date, string format)
        {
            if (string.IsNullOrEmpty(date)) return null;

            return DateTime.ParseExact(date, format, null);
        }
    }
}