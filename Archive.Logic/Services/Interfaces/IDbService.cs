using Archive.Data;
using Archive.Data.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IDbService
    {
        IUnitOfWork<ArchiveContext> UnitOfWork { get; }
    }
}
