using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrinetixInterview.ApplicationServices.Tests
{
    [TestClass]
    public class BrowserTest
    {
        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_DirectoryContainerLocationIsNull()
        {
            DirectoryRecursiveBrowser browser = new DirectoryRecursiveBrowser();
            browser.Browse(null);           
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_FileContainerLocationIsNull()
        {
            FileLinesBrowser browser = new FileLinesBrowser();
            browser.Browse(null);
        }
    }
}
