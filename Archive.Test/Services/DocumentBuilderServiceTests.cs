using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Archive.Logic.Services;
using Archive.Logic.Interfaces;
using Archive.Logic.Documents;

namespace Archive.Test.Services
{
    public class DocumentBuilderServiceTests
    {
        private readonly string _filename;

        
        public DocumentBuilderServiceTests()
        {
            _filename = @"C:\Users\SilentHill\Desktop\Тестовые файлы\Конфиг.txt";
        }


        [Test]
        public void Should_Currect_Building_Documents_With_File_Test()
        {
            DocumentBuilderService builder = new();

            List<ITextDocument> expected = GetExpectedCollection().OrderBy(x => x.Number).ToList();
            List<ITextDocument> actual = builder.Build(_filename).OrderBy(x => x.Number).ToList();

            if (!EqualsDocumentCollections(expected, actual))
                Assert.Fail("Коллекции не эквивалентны!");
        }

        [Test]
        public void Should_Currect_Building_Documents_ParsingService_Test()
        {
            DocumentBuilderService builder = new();
            ParsingService parser = new(_filename);

            List<ITextDocument> expected = GetExpectedCollection().OrderBy(x => x.Number).ToList();

            List<ITextDocument> actual = builder.Build(parser).OrderBy(x => x.Number).ToList();

            if (!EqualsDocumentCollections(expected, actual))
                Assert.Fail("Коллекции не эквивалентны!");
        }

        private static bool EqualsDocumentCollections(List<ITextDocument> a, List<ITextDocument> b)
        {
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                    return false;

                for (int j = 0; j < a[i].Documents?.Count; j++)
                {
                    if (!a[i].Documents![j].Equals(b[i].Documents[j]))
                        return false;
                }
            }

            return true;
        }

        private static List<ITextDocument> GetExpectedCollection()
        {
            List<IDocumentInfo> documentInfos = Help.GetDocumentInfoCollection();
            List<ITextDocument> result = new();

            foreach (IDocumentInfo documentInfo in documentInfos)
            {
                WordDocument word = new(documentInfo);
                word.SetDocuments(documentInfo.References.Select(x => new WordDocument(x)));

                result.Add(word);

            }

            return result;
        }
    }
}
