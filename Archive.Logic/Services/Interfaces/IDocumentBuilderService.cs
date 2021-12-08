using System;
using System.Collections.Generic;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Services.Interfaces
{
    public interface IDocumentBuilderService<T> : IService
    {
        event Action<T>? Builded;

        List<T> Build(string filename);

        List<T> Build(IParsingService parser);
    }
}
