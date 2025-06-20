using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public static class ConsoleOutputWriter
    {
        /// <summary>
        /// Writes the list of discarded scan IDs in ascending order, comma-separated.
        /// </summary>
        /// <param name="discardedScanIds">List of scan IDs to be printed.</param>
        public static void WriteDiscardedScanIds(List<int> discardedScanIds)
        {
            discardedScanIds.Sort();
            Console.WriteLine(string.Join(",", discardedScanIds));
        }
    }
}