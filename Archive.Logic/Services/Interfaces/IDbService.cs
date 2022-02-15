using Archive.Data;
using Archive.Data.Entities;
using Archive.Data.Interfaces;

using System.Collections.Generic;

namespace Archive.Logic.Services.Interfaces
{
    public interface IDbService : IService
    {
        IUnitOfWork<ArchiveContext> UnitOfWork { get; }

        List<Document> GetAll();
        bool DropDatabase();
        bool CreateDatabase();
    }
}
