// File: Infrastructure/Parsers/DocumentParser.cs
using DocScanFilter.Domain.Entities;
using DocScanFilter.Domain.Enums;
using DocScanFilter.Domain.Interfaces;
using System;
using System.Globalization;

namespace DocScanFilter.Infrastructure.Parsers
{
    public class DocumentParser : IDocumentParser
    {
        public Document Parse(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 9)
                throw new FormatException("Line must contain exactly 9 fields.");

            if (!Enum.TryParse(parts[1], out DocumentType docType))
                throw new FormatException("Invalid document type.");

            return new Document(
                scanId: int.Parse(parts[0]),
                documentType: docType,
                issuingCountry: parts[2].Substring(0, 3).ToUpper(),
                lastName: parts[3].Trim(),
                firstName: parts[4].Trim(),
                documentNumber: parts[5].Trim(),
                nationality: parts[6].Substring(0, 3).ToUpper(),
                dateOfBirth: ParseDate(parts[7]),
                dateOfExpiry: ParseDate(parts[8])
            );
        }

        private DateTime ParseDate(string dateStr)
        {
            if (dateStr.Length != 6 || !DateTime.TryParseExact(dateStr, "yyMMdd", null, DateTimeStyles.None, out var date))
                throw new FormatException($"Invalid date format: {dateStr}");

            return date;
        }
    }
}
