using System.Collections.Generic;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IMapperService
    {
        List<TEntity> Map<TEntity, IRelatedEntity>(IEnumerable<ITextDocument> documents);
    }
}
