namespace DocumentFilteringApp.Domain.Entities
{
    /// <summary>
    /// Represents a scanned identity document with relevant extracted fields.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Unique identifier for the scan.
        /// </summary>
        public int ScanId { get; set; }

        /// <summary>
        /// Type of the document (e.g., P = Passport, ID = Identity Card, DL = Driver License).
        /// </summary>
        public string DocumentType { get; set; } = string.Empty;

        /// <summary>
        /// Country that issued the document (ICAO 3-letter format).
        /// </summary>
        public string IssuingCountry { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the document holder.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// First name of the document holder.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Document number (numeric or alphanumeric).
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Nationality of the person (ICAO 3-letter format).
        /// </summary>
        public string Nationality { get; set; } = string.Empty;

        /// <summary>
        /// Date of birth in YYMMDD format.
        /// </summary>
        public string DateOfBirth { get; set; } = string.Empty;

        /// <summary>
        /// Document expiration date in YYMMDD format.
        /// </summary>
        public string ExpirationDate { get; set; } = string.Empty;

        /// <summary>
        /// Checks if a date string in YYMMDD format is valid.
        /// Assumes all dates are from 21st century.
        /// </summary>
        public bool IsValidDate(string date)
        {
            return DateTime.TryParseExact("20" + date, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out _);
        }

        /// <summary>
        /// Returns true if the document is expired or has an invalid expiration date.
        /// </summary>
        public bool IsExpired()
        {
            return !IsValidDate(ExpirationDate) ||
                DateTime.ParseExact("20" + ExpirationDate, "yyyyMMdd", null) < DateTime.Today;
        }

        /// <summary>
        /// Returns the first three characters of the issuing country, uppercased.
        /// Trims invalid suffixes (e.g., 'USAXX' → 'USA').
        /// </summary>
        public string NormalizedIssuingCountry => IssuingCountry[..3].ToUpper();

        /// <summary>
        /// Returns the first three characters of the nationality, uppercased.
        /// </summary>
        public string NormalizedNationality => Nationality[..3].ToUpper();
    }
}