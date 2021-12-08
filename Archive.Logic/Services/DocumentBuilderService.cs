using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Archive.Data.Entities;
using Archive.Logic.Documents;
using Archive.Logic.Exceptions;
using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class DocumentBuilderService : IDocumentBuilderService<Document>
    {
        private readonly IMapperService _mapperService;
        private readonly ICachedCollection<ReferenceDocument> _cachedDocuments;
        private readonly object _locker;


        public DocumentBuilderService()
        {
            _locker = new object();
            _cachedDocuments = new CachedCollection<ReferenceDocument>();
            _mapperService = ServiceFactory.GetService<IMapperService>();
        }


        public event Action<Document>? Builded;


        public List<Document> Build(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            if (string.IsNullOrWhiteSpace(filename))
                return new List<Document>();

            IParsingService parser = ServiceFactory.GetService<IParsingService>(filename);

            return Build(parser);
        }

        public List<Document> Build(IParsingService parser)
        {
            List<IDocumentInfo> documentInfos = parser.Parse();

            List<Document> result = documentInfos
                .Select(x => BuildDocument(x))
                .ToList();

            return result;
        }

        private Document BuildDocument(IDocumentInfo documentInfo)
        {
            Document document = _mapperService.Map<Document, ReferenceDocument>(Create(documentInfo));

            document.RefDocuments = BuildReferenceDocuments(documentInfo.References);

            Builded?.Invoke(document);

            return document;
        }

        private List<ReferenceDocument> BuildReferenceDocuments(List<IDocumentInfo>? documentInfos)
        {
            if (documentInfos is null)
                return new List<ReferenceDocument>();

            List<ReferenceDocument> references = new();

            foreach (IDocumentInfo documentInfo in documentInfos)
            {
                ReferenceDocument? document = _cachedDocuments.Find(documentInfo.RootDocument.Name);

                if (document is not null)
                {
                    Debug.WriteLine($"Finded document: {document.Number}");
                    references.Add(document);
                    continue;
                }

                ReferenceDocument refDocument = _mapperService.Map<ReferenceDocument, Document>(Create(documentInfo));
                Debug.WriteLine($"Created document: {refDocument.Number}");
                references.Add(refDocument);
                _cachedDocuments.Add(refDocument);
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
