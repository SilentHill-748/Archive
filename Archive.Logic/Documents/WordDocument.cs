using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Interfaces;

using Microsoft.Office.Interop.Word;

namespace Archive.Logic.Documents
{
    public class WordDocument : ITextDocument, IPrintableDocument
    {
        private readonly IDocumentInfo _documentInfo;
        private readonly Application _wordApplication;
        private readonly Document _currentDocument;
        private bool disposedValue;


        public WordDocument(IDocumentInfo documentInfo)
        {
            ArgumentNullException.ThrowIfNull(documentInfo);

            _documentInfo = documentInfo;
            _wordApplication = new Application();
            _currentDocument = _wordApplication.Documents
                .Add(_documentInfo.RootDocument.FullName);
        }


        public int Number
        {
            get
            {
                string stringNumber = _documentInfo.RootDocument.Name.Split(',')[0];
                
                if (int.TryParse(stringNumber, out int number))
                    return number;

                throw new Exception("Файл не содержит номера!");
            }
        }

        public string Title => 
            _documentInfo.RootDocument.Name;

        public string Text => 
            _currentDocument.Content.Text;

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

        public void Print()
        {
            _currentDocument.PrintOut();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _currentDocument.Close(SaveChanges: true);
                    _wordApplication.Quit(SaveChanges: true);
                    Documents?.Clear();
                }

                disposedValue = true;
            }
        }
    }
}
