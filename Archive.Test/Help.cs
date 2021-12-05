using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using Archive.Logic.Documents;
using Archive.Logic.Exceptions;
using Archive.Logic.Interfaces;
using Archive.Test.Services;
using Archive.Test.TestModels;

namespace Archive.Test
{
    internal static class Help
    {
        internal static List<IDocumentInfo> GetDocumentInfoCollection()
        {
            XmlSerializer serializer = new(typeof(Arguments));

            Arguments? documentArgumentsCollection = (Arguments?)serializer.Deserialize(GetXmlFileStream());

            if (documentArgumentsCollection is null)
                throw new Exception("Не получается десериализовать xml файл!");

            List<IDocumentInfo> result = new();

            foreach (DocumentArguments arguments in documentArgumentsCollection.DocumentArgs)
            {
                result.Add(new DocumentInfo(new string[]
                {
                    arguments.RootDocument,
                    arguments.KeyWords,
                    string.Join(",", arguments.References)
                }));
            }

            return result;
        }

        private static Stream GetXmlFileStream()
        {
            string xmlResourcePath = "Archive.Test.TestModels.TestExpectedArguments.xml";

            Stream? xmlStream = typeof(ParsingServiceTests).Assembly
                .GetManifestResourceStream(xmlResourcePath);

            if (xmlStream is null)
                throw new CannotReadManifestResourceStream();

            return xmlStream;
        }
    }
}
