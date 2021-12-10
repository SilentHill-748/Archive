using System;
using System.Collections.Generic;

using Archive.Data.Entities;

namespace Archive.Logic.Services.Interfaces
{
    public interface IPrintService : IService, IDisposable
    {
        public void PrintDocuments(IEnumerable<Document> documents);

        public void PrintDocument(Document document);
    }
}
