using DocumentFilteringApp.Domain.Entities;

namespace DocumentFilteringApp.Domain.Services
{
    /// <summary>
    /// Provides domain-level validation rules for identity documents.
    /// </summary>
    public static class DocumentValidator
    {
        /// <summary>
        /// List of accepted countries for both issuing country and nationality.
        /// Based on ICAO 3-letter codes for Spain and neighboring countries.
        /// </summary>
        private static readonly HashSet<string> AcceptedCountries = new()
        {
            "ESP", // Spain
            "FRA", // France
            "POR", // Portugal
            "AND", // Andorra
            "MOR"  // Morocco
        };

        /// <summary>
        /// Validates if the issuing country and nationality are both acceptable.
        /// </summary>
        /// <param name="doc">The document to validate.</param>
        /// <returns>True if both issuing country and nationality are valid.</returns>
        public static bool IsCountryAccepted(Document doc)
        {
            return AcceptedCountries.Contains(doc.NormalizedIssuingCountry)
                && AcceptedCountries.Contains(doc.NormalizedNationality);
        }
    }
}