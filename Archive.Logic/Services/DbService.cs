using Archive.Data;
using Archive.Data.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class DbService : IDbService
    {
        public DbService()
        {
            UnitOfWork = new UnitOfWork<ArchiveContext>(new ArchiveContextFactory().CreateDbContext());
        }


        public IUnitOfWork<ArchiveContext> UnitOfWork { get; }
    }
}
