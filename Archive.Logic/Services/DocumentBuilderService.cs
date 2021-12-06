using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Documents;
using Archive.Logic.Exceptions;
using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class DocumentBuilderService : IDocumentBuilderService
    {
        private readonly ICachedCollection<ITextDocument> _cachedDocuments;
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
                .Select(x => BuildDocument(x))
                .ForAll(y =>
                {
                    lock (_locker)
                    {
                        result.Add(y);
                        Builded?.Invoke(y);
                    }
                });

            return result;
        }

        private ITextDocument BuildDocument(IDocumentInfo documentInfo)
        {
            ITextDocument textDocument = Create(documentInfo);

            textDocument.RefDocuments = BuildReferenceDocuments(documentInfo.References);
            
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

                ITextDocument textDocument = Create(documentInfo);
                references.Add(textDocument);
                _cachedDocuments.Add(textDocument);
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
    }
}
