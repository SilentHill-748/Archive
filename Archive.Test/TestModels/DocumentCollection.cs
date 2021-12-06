using System.Collections.Generic;
using System.Xml.Serialization;

namespace Archive.Test.TestModels
{
    [XmlRoot("Documents")]
    public class DocumentCollection
    {
        [XmlElement("Document")]
        public List<Document> Documents { get; set; } = new();
    }

    [XmlRoot("Document")]
    public class Document
    {
        [XmlAttribute("Number")]
        public int Number { get; set; }
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Path")]
        public string Path { get; set; }
        [XmlIgnore]
        public string Text { get; set; }
        [XmlAttribute("KeyWords")]
        public string KeyWords { get; set; }

        [XmlElement("ReferenceDocuments")]
        public ReferenceCollection ReferenceCollection { get; set; } = new();

        [XmlIgnore]
        public List<ReferenceDocument> References { get; set; } = new();
    }

    [XmlRoot("ReferenceDocuments")]
    public class ReferenceCollection
    {
        [XmlElement("ReferenceDocument")]
        public List<ReferenceDocument> References { get; set; } = new();
    }

    [XmlRoot("ReferenceDocuments")]
    public class ReferenceDocument
    {
        [XmlAttribute("Number")]
        public int Number { get; set; }
        [XmlAttribute("Title")]
        public string Title { get; set; }
        [XmlAttribute("Path")]
        public string Path { get; set; }

        [XmlIgnore]
        public List<Document> Documents { get; set; }
    }
}
