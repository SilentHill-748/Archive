using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Archive.Logic.Documents;
using Archive.Logic.Services;
using Archive.Data.Entities;

namespace Archive.Test.Documents
{
    public class DocumentCollectionTests
    {
        [Test]
        public void ShouldAddDocumentToCollectionTest()
        {
            using DocumentCollection collection = new();

            var expected = GetTestDocuments();

            foreach (Document document in expected)
                collection.Add(document);

            Assert.IsTrue(collection.Documents.Count == 2);
            Assert.IsTrue(collection.Documents[0].RefDocuments.Count == 2);
            Assert.IsTrue(collection.Documents[1].RefDocuments.Count == 2);

            for (int i = 0; i < collection.Documents.Count; i++)
            {
                if (!(collection.Documents[i].Title == expected[i].Title))
                    Assert.Fail($"Документы не экивалентны! Индекс: {i}");

                for (int j = 0; j < collection.Documents[i].RefDocuments.Count; j++)
                    if (!(collection.Documents[i].RefDocuments[j].Title == expected[i].RefDocuments[j].Title))
                        Assert.Fail($"Ссылочные документы не эквивалентны! Индекс: {j}");
            }
        }

        [Test]
        public void ShouldRemoveDocumentFromDocumentCollectionTest()
        {
            using DocumentCollection collection = GetCollection();

            collection.Remove(collection.Documents[0]);

            Assert.IsTrue(collection.Documents.Count == 1);
            Assert.AreEqual("2.Test2.pdf", collection.Documents[0].Title);
        }

        [Test]
        public void ShouldRemoveDocumentByNumberFromDocumentCollectionTest()
        {
            using DocumentCollection collection = GetCollection();

            collection.Remove(1);

            Assert.IsTrue(collection.Documents.Count == 1);
            Assert.AreEqual("2.Test2.pdf", collection.Documents[0].Title);
        }

        [Test]
        public void ShouldMoveUpDocumentAtDocumentCollectionTest()
        {
            using DocumentCollection collection = GetCollection();

            collection.MoveUp(collection.Documents[1]);

            Assert.AreEqual("2.Test2.pdf", collection.Documents[0].Title);
            Assert.AreEqual("1.Test1.rtf", collection.Documents[1].Title);
        }

        [Test]
        public void ShouldMoveDownDocumentAtDocumentCollectionTest()
        {
            using DocumentCollection collection = GetCollection();

            collection.MoveDown(collection.Documents[0]);

            Assert.AreEqual("2.Test2.pdf", collection.Documents[0].Title);
            Assert.AreEqual("1.Test1.rtf", collection.Documents[1].Title);
        }

        [Test]
        public void ShouldSaveDocumentCollectionToXmlFileTest()
        {
            string savedXmlFilePath = "F:\\SavedTestCollection2.xml";
            using DocumentCollection expected = GetCollection();

            expected.SaveCollection(savedXmlFilePath);

            FileAssert.Exists(savedXmlFilePath);
        }

        [Test]
        public void ShouldLoadDocumentCollectionFromXmlFileTest()
        {
            string savedXmlFilePath = "F:\\SavedTestCollection.xml";
            
            using DocumentCollection expected = GetCollection();
            using DocumentCollection actual = new();

            actual.LoadCollection(savedXmlFilePath);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void ShouldExportDocumentCollectionToTextFileTest()
        {
            using DocumentCollection collection = GetCollection();

            string textfilePath = "F:\\TestExport.txt";

            collection.ExportCollection(textfilePath);

            FileAssert.Exists(textfilePath);

            StringAssert.AreEqualIgnoringCase(
                System.IO.File.ReadAllLines(textfilePath)[0],
                @"1: 1.Test1.rtf - C:\Users\SilentHill\Desktop\TestDocuments\1.Test1.rtf");

            StringAssert.AreEqualIgnoringCase(
                System.IO.File.ReadAllLines(textfilePath)[1],
                @"2: 2.Test2.pdf - C:\Users\SilentHill\Desktop\TestDocuments\2.Test2.pdf");
        }

        [Test]
        public void ShouldClearDocumentCollectionTest()
        {
            using DocumentCollection collection = GetCollection();

            Assert.IsTrue(collection.Documents.Count > 0);

            collection.Clear();
            Assert.IsTrue(collection.Documents.Count == 0);
        }

        private static DocumentCollection GetCollection()
        {
            DocumentCollection collection = new();

            var documents = GetTestDocuments();

            foreach (Document document in documents)
                collection.Add(document);

            return collection;
        }

        private static List<Document> GetTestDocuments()
        {
            string path = @"C:\Users\SilentHill\Desktop\TestDocuments\Конфиг.txt";

            DocumentBuilderService builder = new();
            return builder.Build(path).OrderBy(x => x.Number).ToList();
        }
    }
}
