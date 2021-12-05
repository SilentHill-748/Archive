using System.Collections.Generic;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IParsingService
    {
        List<IDocumentInfo> Parse();
    }
}
