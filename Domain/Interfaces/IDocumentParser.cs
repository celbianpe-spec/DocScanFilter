using DocScanFilter.Domain.Entities;

namespace DocScanFilter.Domain.Interfaces
{


    public interface IDocumentParser
    {
        Document Parse(string line);
    }
}