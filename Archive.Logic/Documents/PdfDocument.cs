﻿using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Logic.Interfaces;

using Pdf = BitMiracle.Docotic.Pdf;

namespace Archive.Logic.Documents
{
    public sealed class PdfDocument : ITextDocument
    {
        private readonly IDocumentInfo _documentInfo;
        private readonly Pdf.PdfDocument _pdfDocument;
        private bool disposedValue;


        public PdfDocument(IDocumentInfo documentInfo)
        {
            ArgumentNullException.ThrowIfNull(documentInfo, nameof(documentInfo));

            _documentInfo = documentInfo;
            _pdfDocument = new Pdf.PdfDocument(_documentInfo.RootDocument.FullName);
            RefDocuments = new List<ITextDocument>();
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
        public string Title => _documentInfo.RootDocument.Name;
        public string Text => GetText();
        public string Path => _documentInfo.RootDocument.FullName;
        public string? KeyWords => _documentInfo.KeyWords;
        public List<ITextDocument>? RefDocuments { get; set; }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _pdfDocument.Dispose();
                disposedValue = true;
            }
        }

        private string GetText()
        {
            Pdf.PdfPage page = _pdfDocument.Pages[0];

            if (page.GetImages().Any())
                return GetTextFromImageOnFirstPage(page.GetImages().First());

            return _pdfDocument.GetText();
        }

        // Получить текст из первого скана на первой странице с применением Tesseract.
        private string GetTextFromImageOnFirstPage(Pdf.PdfImage image)
        {
            // Заглушка
            return string.Empty;
        }
    }
}
