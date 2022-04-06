namespace HCA.Feature.Account.Models.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class DateValidator
    {
        private const string Pattern = @"^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$";
        public static ValidationResult Validate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return ValidationResult.Success;
            }

            var reg = new Regex(Pattern);

            if (reg.IsMatch(date))
            {
                if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
                {
                    if (dateTime.ToUniversalTime() < DateTime.UtcNow)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult("Date has invalid format");
        }
    }
}