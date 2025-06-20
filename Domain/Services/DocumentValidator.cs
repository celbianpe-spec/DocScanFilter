using DocScanFilter.Domain.Entities;

namespace DocScanFilter.Domain.Services
{
    /// <summary>
    /// Service responsible for validating document properties according to business rules.
    /// </summary>
    public class DocumentValidator
    {
        private static readonly HashSet<string> AcceptedNationalities = new() { "ESP", "FRA", "POR", "AND", "MOR" };

        /// <summary>
        /// Validates whether a document meets all business validation rules.
        /// </summary>
        public bool IsValid(Document doc)
        {
                return IsValidNationality(doc.Nationality)
                    && IsValidIssuingCountry(doc.IssuingCountry)
                    && (doc.DateOfExpiry.HasValue && IsNotExpired(doc.DateOfExpiry.Value))
                    && (doc.DateOfBirth.HasValue && IsValidDate(doc.DateOfBirth.Value))
                    && (doc.DateOfExpiry.HasValue && IsValidDate(doc.DateOfExpiry.Value));
            }

        /// <summary>
        /// Returns true if the nationality is one of the accepted ICAO codes.
        /// </summary>
        private bool IsValidNationality(string nationality)
        {
            if (string.IsNullOrWhiteSpace(nationality) || nationality.Length < 3)
                return false;

            return AcceptedNationalities.Contains(nationality[..3].ToUpper());
        }

        /// <summary>
        /// Returns true if the issuing country is valid.
        /// </summary>
        private bool IsValidIssuingCountry(string issuingCountry)
        {
            if (string.IsNullOrWhiteSpace(issuingCountry) || issuingCountry.Length < 3)
                return false;

            return AcceptedNationalities.Contains(issuingCountry[..3].ToUpper());
        }

        /// <summary>
        /// Returns true if the document has not expired.
        /// </summary>
        private bool IsNotExpired(DateTime expiryDate)
        {
            return expiryDate > DateTime.Now;
        }

        /// <summary>
        /// Returns true if the date is valid and not in the future.
        /// </summary>
        private bool IsValidDate(DateTime date)
        {
            return date.Year >= 2000 && date <= DateTime.Now.AddYears(50);
        }
    }
}
