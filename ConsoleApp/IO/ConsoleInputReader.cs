using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class ConsoleInputReader
    {
        /// <summary>
        /// Reads the input from the console and returns a list of input lines.
        /// </summary>
        /// <returns>List of strings representing the input lines</returns>
        public List<string> ReadInputLines()
        {
            var inputLines = new List<string>();

            if (!int.TryParse(Console.ReadLine(), out int numberOfLines))
            {
                Console.WriteLine("Invalid number of records.");
                return inputLines;
            }

            for (int i = 0; i < numberOfLines; i++)
            {
                string? line = Console.ReadLine();
                if (line != null)
                {
                    inputLines.Add(line);
                }
            }

            return inputLines;
        }
    }
}
