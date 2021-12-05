using System;
using System.Collections.Generic;

using NUnit.Framework;

using Archive.Logic.Services;
using Archive.Logic.Interfaces;

namespace Archive.Test.Services
{
    public class ParsingServiceTests
    {
        private readonly string _filename;


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

            List<IDocumentInfo> expected = Help.GetDocumentInfoCollection();

            List<IDocumentInfo> actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
