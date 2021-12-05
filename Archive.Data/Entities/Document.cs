using System.Collections.Generic;

namespace Archive.Data.Entities
{
    public class Document
    {
        public Document() { }


        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string KeyWords { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public List<ReferenceDocument> RefDocuments { get; set; } = new();
    }
}
