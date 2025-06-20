using DocumentFilteringApp.Domain.Entities;

namespace DocumentFilteringApp.Domain.Interfaces
{


    public interface IDocumentParser
    {
        Document Parse(string line);
    }
}