using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices.Tests
{
    [TestClass]
    public class UnnormalizedLocationProcessorTest
    {
        private Mock<IContainerBrowser> _directoryRecursiveBrowser;
        private Mock<IContainerBrowser> _fileLinesBrowser;
        private Mock<IBrowserFilter> _directoryRecursiveBrowserFilter;
        private Mock<IBrowserFilter> _fileLinesBrowserFilter;

        [TestInitialize]
        [TestCategory("Unit")]
        public void Init()
        {
            _directoryRecursiveBrowser = new Mock<IContainerBrowser>();
            _fileLinesBrowser = new Mock<IContainerBrowser>();
            _directoryRecursiveBrowserFilter = new Mock<IBrowserFilter>();
            _fileLinesBrowserFilter = new Mock<IBrowserFilter>();

            _directoryRecursiveBrowserFilter.Setup(x => x.Pattern).Returns("pattern1");
            _directoryRecursiveBrowser.Setup(x => x.Browse(It.IsAny<string>(), _directoryRecursiveBrowserFilter.Object)).Returns(new[] { @"c:\test\1.txt", @"c:\test\2.txt", @"c:\test\3.txt" });

            _fileLinesBrowserFilter.Setup(x => x.Pattern).Returns("pattern2");
            _fileLinesBrowser.Setup(x => x.Browse(It.IsAny<string>(), _fileLinesBrowserFilter.Object)).Returns(new[] { "aaa", "bbb", "ccc" });
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_Be3LinesInFile()
        {
            var parser = new Mock<IStringParser>();
            TextFilesProcessor processor = new TextFilesProcessor(parser.Object, _directoryRecursiveBrowser.Object, _fileLinesBrowser.Object, _directoryRecursiveBrowserFilter.Object, _fileLinesBrowserFilter.Object);
            processor.PopulateUnnormalizedData("path to file here");

            _fileLinesBrowser.Verify(lw => lw.Browse(It.IsAny<string>(), _fileLinesBrowserFilter.Object), Times.Exactly(3));           
        }
    }
}
