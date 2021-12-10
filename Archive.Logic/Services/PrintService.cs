using System;
using System.Collections.Generic;

using Archive.Data.Entities;
using Archive.Logic.Services.Interfaces;

using Word = Microsoft.Office.Interop.Word;

namespace Archive.Logic.Services
{
    public class PrintService : IPrintService
    {
        private readonly Word.Application _wordApplication;
        private bool disposedValue;


        public PrintService()
        {
            _wordApplication = new Word.Application();
        }


        public void PrintDocument(Document document)
        {
            Word.Document printDocument = _wordApplication.Documents.Open(document.Path);
            printDocument.PrintOut();
            printDocument.Close();
        }

        public void PrintDocuments(IEnumerable<Document> documents)
        {
            foreach (Document document in documents)
                PrintDocument(document);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _wordApplication.Quit();
                }

                disposedValue = true;
            }
        }
    }
}
