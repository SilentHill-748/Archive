using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Interfaces;

using Spire.Doc;

namespace Archive.Logic.Documents
{
    public class WordDocument : ITextDocument
    {
        private readonly IDocumentInfo _documentInfo;
        private readonly Document _currentDocument;
        private bool disposedValue;


        public WordDocument(IDocumentInfo documentInfo)
        {
            ArgumentNullException.ThrowIfNull(documentInfo);

            _documentInfo = documentInfo;
            _currentDocument = new Document(documentInfo.RootDocument.FullName);
        }


        public int Number
        {
            get
            {
                string stringNumber = _documentInfo.RootDocument.Name.Split('.')[0];
                
                if (int.TryParse(stringNumber, out int number))
                    return number;

                throw new Exception("Файл не содержит номера!");
            }
        }

        public string Title => 
            _documentInfo.RootDocument.Name;

        public string Text => 
            _currentDocument.GetText();

        public string Path => 
            _documentInfo.RootDocument.FullName;

        public string? KeyWords => 
            _documentInfo.KeyWords;

        public List<ITextDocument>? Documents { get; set; }


        public void SetDocuments(IEnumerable<ITextDocument> documents)
        {
            ArgumentNullException.ThrowIfNull(documents, nameof(documents));

            Documents = documents.ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is ITextDocument doc)
            {
                return  this.Number == doc.Number &&
                        this.Title == doc.Title &&
                        this.Path == doc.Path;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _currentDocument.Close();
                    _currentDocument.Dispose();
                    Documents?.Clear();
                }

                disposedValue = true;
            }
        }
    }
}
