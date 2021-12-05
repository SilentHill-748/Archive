using Archive.Logic.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Logic.Documents
{
    //TODO: Добавить библиотеку работы с PDF.
    public sealed class PdfDocument : ITextDocument, IPrintableDocument
    {
        private readonly IDocumentInfo _documentInfo;
        private bool disposedValue;
        private bool disposedValue1;

        public PdfDocument(IDocumentInfo documentInfo)
        {
            ArgumentNullException.ThrowIfNull(documentInfo, nameof(documentInfo));

            _documentInfo = documentInfo;

            Text = Path = KeyWords = Title = string.Empty; // Заглушка
        }


        public int Number { get; }
        public string Title { get; }
        public string Text { get; }
        public string Path { get; }
        public string? KeyWords { get; }
        public List<ITextDocument>? Documents { get; set; }


        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue1)
            {
                if (disposing)
                {
                    
                }

                disposedValue1 = true;
            }
        }
    }
}
