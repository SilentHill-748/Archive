using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;


using Archive.Data.Entities;
using Archive.Logic.Interfaces;
using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Archive.Test.Services
{
    public class MapperServiceTests
    {
        private readonly IParsingService _parser;
        private readonly IDocumentBuilderService _builder;
        private readonly IMapperService _mapper;


        public MapperServiceTests()
        {
            string path = @"C:\Users\SilentHill\Desktop\Тестовые файлы\Конфиг.txt";

            _parser = ServiceFactory.GetService<ParsingService, IParsingService>(path);
            _builder = ServiceFactory.GetService<DocumentBuilderService, IDocumentBuilderService>();
            _mapper = ServiceFactory.GetService<MapperService, IMapperService>();
        }


        [Test]
        public void Should_Correct_Mapped_ITextDocument_To_Document_EntityTest()
        {
            using ITextDocument document = _builder.Build(_parser).First(x => x.Number == 1);

            var actual = _mapper.Map<Document, ReferenceDocument>(new ITextDocument[] { document })[0];

            AssertDocument(actual);
        }

        [Test]
        public void Should_Correct_Mapped_All_ITextDocument_Collection_To_Documents_Test()
        {
            List<ITextDocument> documents = _builder.Build(_parser);

            var actual = _mapper.Map<Document, ReferenceDocument>(documents);

            foreach (Document document in actual)
                AssertDocument(document);
        }

        private void AssertDocument(Document document)
        {
            Assert.IsTrue(document.RefDocuments.Count > 0);
            Assert.IsTrue(Regex.IsMatch(document.KeyWords, @"(\w*)\,(\w*)"));
            Assert.IsTrue(document.Number != 0);
            Assert.IsTrue(document.Path == $@"C:\Users\SilentHill\Desktop\Тестовые файлы\{document.Title}");
            Assert.IsTrue(Regex.IsMatch(document.Title, @"\d*\.\w*\.(pdf|doc|docx|odt|rtf)"));
        }
    }
}
