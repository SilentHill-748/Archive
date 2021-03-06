using System;

using NUnit.Framework;

using Archive.Logic.Services.Interfaces;
using Archive.Logic.Services;
using Archive.Logic.Exceptions;

using Archive.Data.Entities;

namespace Archive.Test.Services
{
    public class ServiceFactoryTests
    {
        [Test]
        public void Should_Throw_TypeNotFoundException_Test()
        {
            Assert.Throws<TypeNotFoundException>(() => 
                ServiceFactory.GetService<ISearchService>());
        } 

        [Test]
        public void Should_Return_IDocumentBuilderService_Test()
        {
            Type expected = typeof(DocumentBuilderService);
            Type actual = ServiceFactory.GetService<IDocumentBuilderService<Document>>().GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return_IParsingService_Test()
        {
            string filename = "test record";

            Type expected = typeof(ParsingService);
            Type actual = ServiceFactory.GetService<IParsingService>(filename).GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return_IMapperService_Test()
        {
            Type expected = typeof(MapperService);
            Type actual = ServiceFactory.GetService<IMapperService>().GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Throw_Exception_Test()
        {
            Assert.Throws<Exception>(() => 
                ServiceFactory.GetService<DocumentBuilderService>());
        }
    }

    public class Test<T>
    {
        public Test() { }
    }
}
