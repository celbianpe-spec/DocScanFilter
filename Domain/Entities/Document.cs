using System;
using DocScanFilter.Domain.Enums;

namespace DocScanFilter.Domain.Entities
{
    public class Document
    {
        public int ScanId { get; }
        public DocumentType DocumentType { get; }
        public string IssuingCountry { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string DocumentNumber { get; }
        public string Nationality { get; }
        public DateTime? DateOfBirth { get; }
        public DateTime? DateOfExpiry { get; }

        public Document(
            int scanId,
            DocumentType documentType,
            string issuingCountry,
            string lastName,
            string firstName,
            string documentNumber,
            string nationality,
            DateTime? dateOfBirth,
            DateTime? dateOfExpiry)
        {
            if (string.IsNullOrWhiteSpace(issuingCountry))
                throw new ArgumentException("Issuing country is required");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required");
            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new ArgumentException("Document number is required");
            if (string.IsNullOrWhiteSpace(nationality))
                throw new ArgumentException("Nationality is required");

            ScanId = scanId;
            DocumentType = documentType;
            IssuingCountry = NormalizeIcaoCode(issuingCountry);
            LastName = lastName.Trim();
            FirstName = firstName.Trim();
            DocumentNumber = documentNumber.Trim();
            Nationality = NormalizeIcaoCode(nationality);
            DateOfBirth = dateOfBirth;
            DateOfExpiry = dateOfExpiry;
        }

        private string NormalizeIcaoCode(string code)
        {
            return code.Length > 3 ? code.Substring(0, 3).ToUpperInvariant() : code.ToUpperInvariant();
        }

        public bool IsSamePerson(Document other)
        {
            return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(Nationality, other.Nationality, StringComparison.OrdinalIgnoreCase)
                && Nullable.Equals(DateOfBirth, other.DateOfBirth);
        }

        public bool IsSameDocument(Document other)
        {
            return string.Equals(DocumentNumber, other.DocumentNumber, StringComparison.OrdinalIgnoreCase)
                && string.Equals(IssuingCountry, other.IssuingCountry, StringComparison.OrdinalIgnoreCase)
                && DocumentType == other.DocumentType;
        }
    }
}