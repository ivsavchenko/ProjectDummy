using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices.Tests
{
    [TestClass]
    public class FiltersTest
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void Should_ReturnUtf8()
        {
            string expected = "UTF-8";
            IBrowserFilter filter = new Utf8EncodingFilter();
            Assert.AreEqual(expected, filter.Pattern);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_ReturnTxt()
        {
            string expected = "*.txt";
            IBrowserFilter filter = new TextFilesFilter();
            Assert.AreEqual(expected, filter.Pattern);
        }
    }
}
