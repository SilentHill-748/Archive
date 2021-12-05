using System;
using System.Collections.Generic;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IDocumentBuilderService
    {
        event Action<ITextDocument>? Builded;

        List<ITextDocument> Build(string filename);

        List<ITextDocument> Build(IParsingService parser);
    }
}
