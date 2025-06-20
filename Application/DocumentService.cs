using System.Collections.Generic;
using System.Linq;
using DocumentFilteringApp.Domain.Entities;
using DocumentFilteringApp.Domain.Services;

namespace DocumentFilteringApp.Application
{
    public class DocumentService
    {
        private readonly DocumentValidator _validator;

        public DocumentService()
        {
            _validator = new DocumentValidator();
        }

        /// <summary>
        /// Filters out invalid documents based on business rules.
        /// </summary>
        /// <param name="documents">List of scanned documents.</param>
        /// <returns>Sorted list of discarded scan IDs.</returns>
        public List<int> GetDiscardedScanIds(List<Document> documents)
        {
            var discarded = new List<int>();

            foreach (var doc in documents)
            {
                if (!_validator.IsValid(doc))
                {
                    discarded.Add(doc.ScanId);
                }
            }

            // Return IDs in ascending order
            return discarded.OrderBy(id => id).ToList();
        }
    }
}