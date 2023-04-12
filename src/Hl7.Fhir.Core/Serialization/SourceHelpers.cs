using System;
using System.Globalization;

namespace Hl7.Fhir.Serialization
{
    internal static class SourceHelpers
    {
        public static string Truncate(string str)
        {
            const int maxLength = 40;

            if (str == null || str.Length < maxLength)
            {
                return str;
            }
            return str.Substring(0, maxLength - 3) + "...";
        }

        public static bool IsValidDate(string dateString)
        {
            return DateTimeOffset.TryParseExact(dateString, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
        }

        public static bool TryParseFhirInstant(string instantString, out DateTimeOffset instant)
        {
            if (DateTimeOffset.TryParseExact(instantString, _utcFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out instant) ||
                DateTimeOffset.TryParseExact(instantString, _timeZoneFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out instant))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidTime(string timeString)
        {
            return DateTimeOffset.TryParseExact(timeString, _timeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
        }

        private static readonly string[] _dateFormats = new[]
        {
            "yyyy",
            "yyyy'-'MM",
            "yyyy'-'MM'-'dd"
        };

        private static readonly string[] _utcFormats = new[]
        {
                "yyyy'-'MM'-'dd'T'HH':'mm'Z'",
                "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'",
                "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFFFFF'Z'",
        };

        private static readonly string[] _timeZoneFormats = new[]
        {
                "yyyy'-'MM'-'dd'T'HH':'mmzzz",
                "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz",
                "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFFFFFzzz",
        };

        private static readonly string[] _timeFormats = new[]
        {
                "HH':'mm",
                "HH':'mm':'ss",
                "HH':'mm':'ss'.'FFFFFFF",
        };
    }
}
