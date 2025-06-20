using System;
using System.Collections.Generic;
using System.IO;
using DocScanFilter.Application;
using DocScanFilter.Domain.Entities;
using DocScanFilter.Infrastructure.Parsers;

namespace DocScanFilter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new DocumentParser();
            var documents = new List<Document>();

            string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "sample_input.txt");
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("Input file 'sample_input.txt' not found.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length == 0 || !int.TryParse(lines[0], out int n) || n <= 0)
            {
                Console.Error.WriteLine("Invalid or missing record count in input file.");
                return;
            }

            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var document = parser.Parse(line);
                    documents.Add(document);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error parsing line {i + 1}: {ex.Message}");
                }
            }

            var service = new DocumentService();
            var discardedIds = service.GetDiscardedScanIds(documents);

            Console.WriteLine(string.Join(",", discardedIds));
        }
    }
}