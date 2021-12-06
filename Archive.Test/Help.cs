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

            string xmlResourcePath = "Archive.Test.TestModels.TestExpectedArguments.xml";
            Arguments? documentArgumentsCollection = (Arguments?)serializer.Deserialize(GetXmlFileStream(xmlResourcePath));

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

        /// <summary>
        /// Вернет список ожидаемых объектов, которые должны быть получены в тесте маппинга.
        /// </summary>
        /// <returns>Список документов - корректных данных для сравнения.</returns>
        internal static List<Document> GetExpectedMappedDocuments()
        {
            string xmlResourcePath = "Archive.Test.TestModels.ExpectedMappedData.xml";

            Stream xmlStream = GetXmlFileStream(xmlResourcePath);

            XmlSerializer serializer = new(typeof(DocumentCollection));

            DocumentCollection documentCollection = (DocumentCollection?)serializer.Deserialize(xmlStream) ?? 
                throw new Exception("Не получается десериализовать xml файл!");

            foreach (Document document in documentCollection.Documents)
                document.References = document.ReferenceCollection.References;

            return documentCollection.Documents;
        }

        private static Stream GetXmlFileStream(string resourcePath)
        {
            Stream? xmlStream = typeof(ParsingServiceTests).Assembly
                .GetManifestResourceStream(resourcePath);

            if (xmlStream is null)
                throw new CannotReadManifestResourceStream();

            return xmlStream;
        }
    }
}
