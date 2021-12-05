using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

using NUnit.Framework;

using Archive.Logic.Services;
using Archive.Logic.Interfaces;
using Archive.Logic.Documents;
using Archive.Logic.Exceptions;
using Archive.Test.TestModels;

namespace Archive.Test.Services
{
    public class ParsingServiceTests
    {
        private string _filename;


        public ParsingServiceTests()
        {
            _filename = @"C:\Users\SilentHill\Desktop\Тестовые файлы\Конфиг.txt";
        }


        [Test]
        public void Should_Throw_ArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => new ParsingService(null!));
        }

        [Test]
        public void Should_Throw_ArgumentException_Test()
        {
            Assert.Throws<ArgumentException>(() => new ParsingService(""));
        }

        [Test]
        public void Should_Correct_Initialize_ParsingService_Test()
        {
            Assert.DoesNotThrow(() => new ParsingService(_filename));
        }

        [Test]
        public void Should_Correct_Parsing_File_Test()
        {
            ParsingService parser = new(_filename);

            List<IDocumentInfo> expected = GetExpectedCollection();

            List<IDocumentInfo> actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private static List<IDocumentInfo> GetExpectedCollection()
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
