using Archive.Data;
using Archive.Data.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IDbService : IService
    {
        IUnitOfWork<ArchiveContext> UnitOfWork { get; }
    }
}
