using Archive.Logic.Documents;

using NUnit.Framework;

namespace Archive.Test.Documents
{
    public class PdfDocumentTests
    {
        [Test]
        public void ShouldReturnTextFromImageInPdfDocumentTest()
        {
            // With tesseract
            string filename = @"C:\Users\SilentHill\Desktop\TestDocuments\TestPdfDocument.pdf";

            DocumentInfo docInfo = new(new string[] { filename, "", "" });
            PdfDocument pdf = new(docInfo);

            string actual = pdf.Text;

            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
            Assert.IsTrue(actual.Length > 0);
        }

        [Test]
        public void ShouldReturnTextFromPdfDocumentTest()
        {
            // Without tesseract
            string filename = @"C:\Users\SilentHill\Desktop\TestDocuments\6.RefTest4.pdf";

            DocumentInfo docInfo = new(new string[] { filename, "", "" });
            PdfDocument pdf = new(docInfo);

            string expected = "Ref Test 4";
            string actual = pdf.Text;

            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}
