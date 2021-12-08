using System.Collections.Generic;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IMapperService : IService
    {
        List<TEntity> Map<TEntity, IRelatedEntity>(IEnumerable<ITextDocument> documents);

        TEntity Map<TEntity, TRelated>(ITextDocument dcoument);
    }
}
