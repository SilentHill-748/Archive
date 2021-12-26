using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Archive.Data.Entities
{
    public class ReferenceDocument
    {
        public ReferenceDocument() { }


        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        [XmlIgnore]
        public string Text { get; set; } = string.Empty;

        public List<Document> Documents { get; set; } = new();


        public override bool Equals(object? obj)
        {
            if ((obj is not null) &&
                (obj is ReferenceDocument another))
            {
                return  this.Number == another.Number &&
                        this.Title.Equals(another.Title) &&
                        this.Path.Equals(another.Path);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Title, Path);
        }
    }
}