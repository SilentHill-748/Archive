using System.Collections.Generic;
using System.Linq;

using Archive.Data;
using Archive.Data.Entities;
using Archive.Data.Interfaces;
using Archive.Logic.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Archive.Logic.Services
{
    public class DbService : IDbService
    {
        public DbService()
        {
            UnitOfWork = new UnitOfWork<ArchiveContext>(new ArchiveContextFactory().CreateDbContext());
        }


        public IUnitOfWork<ArchiveContext> UnitOfWork { get; }

        public List<Document> GetAll()
        {
            return UnitOfWork
                .GetRepository<Document>()
                .GetAll(d => d.Include(x => x.RefDocuments))
                .ToList();
        }
    }
}
