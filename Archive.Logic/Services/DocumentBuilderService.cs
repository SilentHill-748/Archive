using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Documents;
using Archive.Logic.Exceptions;
using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class DocumentBuilderService : IDocumentBuilderService, IDisposable
    {
        private readonly ICachedCollection<ITextDocument> _cachedDocuments;
        private bool disposedValue;
        private readonly object _locker;


        public DocumentBuilderService()
        {
            _locker = new object();
            _cachedDocuments = new CachedCollection<ITextDocument>();
        }


        public event Action<ITextDocument>? Builded;


        public List<ITextDocument> Build(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            if (string.IsNullOrWhiteSpace(filename))
                return new List<ITextDocument>();

            IParsingService parser = ServiceFactory.GetService<ParsingService, IParsingService>(filename);

            return Build(parser);
        }

        public List<ITextDocument> Build(IParsingService parser)
        {
            List<IDocumentInfo> documentInfos = parser.Parse();
            List<ITextDocument> result = new();

            documentInfos
                .AsParallel()
                .Select(x => BuildDocment(x))
                .ForAll(x =>
                {
                    lock (_locker)
                    {
                        result.Add(x);
                        Builded?.Invoke(x);
                    }
                });

            return result;
        }

        private ITextDocument BuildDocment(IDocumentInfo documentInfo)
        {
            ITextDocument textDocument = Create(documentInfo);
            textDocument.Documents = BuildReferenceDocuments(documentInfo.References);

            return textDocument;
        }

        private List<ITextDocument> BuildReferenceDocuments(List<IDocumentInfo>? documentInfos)
        {
            if (documentInfos is null)
                return new List<ITextDocument>();

            List<ITextDocument> references = new();

            foreach (IDocumentInfo documentInfo in documentInfos)
            {
                ITextDocument? document = _cachedDocuments.Find(documentInfo.RootDocument.Name);

                if (document is not null)
                {
                    references.Add(document);
                    continue;
                }

                document = Create(documentInfo);
                references.Add(document);
                _cachedDocuments.Add(document);
            }

            return references;
        }

        private static ITextDocument Create(IDocumentInfo documentInfo)
        {
            string fileExtension = documentInfo.RootDocument.Extension;

            ITextDocument textDocument = fileExtension switch
            {
                ".doc" or ".docx" or ".odt" or ".rtf" => new WordDocument(documentInfo),
                ".pdf" => new PdfDocument(documentInfo),
                _ => throw new FileFormatNotSupportedException("Формат файла не поддерживается!")
            };

            return textDocument;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cachedDocuments.Clear();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
