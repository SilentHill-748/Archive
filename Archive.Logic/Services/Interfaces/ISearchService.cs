using System.Collections.Generic;

using Archive.Data.Entities;

namespace Archive.Logic.Services.Interfaces
{
    public interface ISearchService : IService
    {
        List<Document> Search(SearchMode mode);
    }
}
