using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrinetixInterview.ApplicationServices.Tests
{
    [TestClass]
    public class StringParserTest
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void Should_SplitParse()
        {
            string text = "Amama - L9Il3w   /   ёйоу";
            SplitStringParser parser = new SplitStringParser();
            string[] result = parser.Parse(text).ToArray();

            Assert.AreEqual("Amama", result[0]);
            Assert.AreEqual("L9Il3w", result[1]);
            Assert.AreEqual("ёйоу", result[2]);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_RegexParse()
        {
            string text = "Amama - L9Il3w      ёйоу";
            RegexStringParser parser = new RegexStringParser();
            string[] result = parser.Parse(text).ToArray();

            Assert.AreEqual("Amama", result[0]);
            Assert.AreEqual("L9Il3w", result[1]);
            Assert.AreEqual("ёйоу", result[2]);
        }
    }
}
