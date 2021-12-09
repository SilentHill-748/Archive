using System.Collections.Generic;

namespace Archive.Data.Entities
{
    public class ReferenceDocument
    {
        public ReferenceDocument() { }


        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public List<Document> Documents { get; set; } = new();
    }
}