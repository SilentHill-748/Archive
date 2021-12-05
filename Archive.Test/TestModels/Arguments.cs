using System.Collections.Generic;
using System.Xml.Serialization;

namespace Archive.Test.TestModels
{
    [XmlRoot("Arguments")]
    public class Arguments
    {
        [XmlElement("DocumentArguments")]
        public List<DocumentArguments> DocumentArgs { get; set; } = new();
    }

    [XmlRoot("DocumentArguments")]
    public class DocumentArguments
    {
        [XmlElement("RootDocument")]
        public string RootDocument { get; set; } = string.Empty;

        [XmlElement("KeyWords")]
        public string KeyWords { get; set; } = string.Empty;

        [XmlArray("References")]
        public List<string> References { get; set; } = new();
    }
}
