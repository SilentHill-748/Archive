using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Archive.Data.Entities
{
    public class Document
    {
        public Document() { }


        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        [XmlIgnore]
        public string Text { get; set; } = string.Empty;
        public string KeyWords { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public List<ReferenceDocument> RefDocuments { get; set; } = new();


        public override bool Equals(object? obj)
        {
            if ((obj is not null) &&
                (obj is Document another))
            {
                return  this.Number == another.Number &&
                        this.Title.Equals(another.Title) &&
                        this.Path.Equals(another.Path) &&
                        RefDocumentsEquals(another.RefDocuments);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Title, Path);
        }

        private bool RefDocumentsEquals(List<ReferenceDocument> anotherRefDocuments)
        {
            for (int i = 0; i < this.RefDocuments.Count; i++)
                if (!this.RefDocuments[i].Equals(anotherRefDocuments[i]))
                    return false;

            return true;
        }
    }
}
