// File: Infrastructure/Parsers/DocumentParser.cs
using DocumentFilteringApp.Domain.Entities;
using DocumentFilteringApp.Domain.Enums;
using DocumentFilteringApp.Domain.Interfaces;
using System;

namespace DocumentFilteringApp.Infrastructure.Parsers
{
    /// <summary>
    /// Parses a line of text into a Document object.
    /// </summary>
    public class DocumentParser : IDocumentParser
    {
        public Document Parse(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 9)
                throw new FormatException("Line must contain exactly 9 fields.");

            return new Document
            {
                ScanId = int.Parse(parts[0]),
                Type = Enum.TryParse<DocumentType>(parts[1], out var docType) ? docType : throw new FormatException("Invalid document type."),
                IssuingCountry = parts[2].Substring(0, 3).ToUpper(),
                LastName = parts[3].Trim(),
                FirstName = parts[4].Trim(),
                DocumentNumber = parts[5].Trim(),
                Nationality = parts[6].Substring(0, 3).ToUpper(),
                DateOfBirth = ParseDate(parts[7]),
                DateOfExpiry = ParseDate(parts[8])
            };
        }

        private DateTime ParseDate(string dateStr)
        {
            if (dateStr.Length != 6 || !DateTime.TryParseExact(dateStr, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out var date))
                throw new FormatException($"Invalid date format: {dateStr}");

            return date;
        }
    }

}
