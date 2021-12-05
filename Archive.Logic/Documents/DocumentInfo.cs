using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Archive.Logic.Interfaces;

namespace Archive.Logic.Documents
{
    public sealed class DocumentInfo : IDocumentInfo
    {
        private DocumentInfo(string filename)
        {
            RootDocument = new FileInfo(filename);
        }

        public DocumentInfo(string[] arguments)
            : this(arguments[0])
        {
            KeyWords = arguments[1];
            References = arguments[2]
                .Split(',')
                .Select(x => (IDocumentInfo)new DocumentInfo(x))
                .ToList();
        }


        public FileInfo RootDocument { get; }
        public string? KeyWords { get; }
        public List<IDocumentInfo>? References { get; }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is DocumentInfo doc)
            {
                return  this.RootDocument.Name.Equals(doc.RootDocument.Name) &&
                        (this.KeyWords is not null && this.KeyWords.Equals(doc.KeyWords));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RootDocument, KeyWords, References);
        }
    }
}
