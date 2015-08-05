using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrinetixInterview.Contracts.Dto;

namespace TrinetixInterview.ApplicationServices.Tests
{
    [TestClass]
    public class PersistorTest
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void Should_CallWordPersistGroup_3Times()
        {
            var context = new Mock<IObjectContextAdapter>();
            var persistor = new Mock<WordsEntityPersistor>(context.Object);
            persistor.Setup(x => x.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()));

            persistor.Object.Persist(
                new[]
                    {
                        new UnnormalizedLocation { Word = "w1" }, new UnnormalizedLocation { Word = "w2" },
                        new UnnormalizedLocation { Word = "w1" }, new UnnormalizedLocation { Word = "w2" },
                        new UnnormalizedLocation { Word = "w3" }, new UnnormalizedLocation { Word = "w1" }
                    });

            persistor.Verify(lw => lw.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()), Times.Exactly(3));           
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_FileNamesPersistGroup_3Times()
        {
            var context = new Mock<IObjectContextAdapter>();
            var persistor = new Mock<FileNamesEntityPersistor>(context.Object);
            persistor.Setup(x => x.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()));

            persistor.Object.Persist(
                new[]
                    {
                        new UnnormalizedLocation { FileName = "w1" }, new UnnormalizedLocation { FileName = "w2" },
                        new UnnormalizedLocation { FileName = "w1" }, new UnnormalizedLocation { FileName = "w2" },
                        new UnnormalizedLocation { FileName = "w3" }, new UnnormalizedLocation { FileName = "w1" }
                    });

            persistor.Verify(lw => lw.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()), Times.Exactly(3));
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_FilePathesPersistGroup_3Times()
        {
            var context = new Mock<IObjectContextAdapter>();
            var persistor = new Mock<FilePathesEntityPersistor>(context.Object);
            persistor.Setup(x => x.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()));

            persistor.Object.Persist(
                new[]
                    {
                        new UnnormalizedLocation { FilePath = "w1" }, new UnnormalizedLocation { FilePath = "w2" },
                        new UnnormalizedLocation { FilePath = "w1" }, new UnnormalizedLocation { FilePath = "w2" },
                        new UnnormalizedLocation { FilePath = "w3" }, new UnnormalizedLocation { FilePath = "w1" }
                    });

            persistor.Verify(lw => lw.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()), Times.Exactly(3));
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Should_FilePathesPersistGroup_Never()
        {
            var context = new Mock<IObjectContextAdapter>();
            var persistor = new Mock<FilePathesEntityPersistor>(context.Object);
            persistor.Setup(x => x.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()));

            persistor.Object.Persist(new UnnormalizedLocation[] { });

            persistor.Verify(lw => lw.PersistGroup(It.IsAny<IGrouping<string, UnnormalizedLocation>>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_FilePathesLocationsParameterIsNull()
        {
            var context = new Mock<IObjectContextAdapter>();
            FilePathesEntityPersistor persistor = new FilePathesEntityPersistor(context.Object);
            persistor.Persist(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_FileNamesLocationsParameterIsNull()
        {
            var context = new Mock<IObjectContextAdapter>();
            FileNamesEntityPersistor persistor = new FileNamesEntityPersistor(context.Object);
            persistor.Persist(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_WordsLocationsParameterIsNull()
        {
            var context = new Mock<IObjectContextAdapter>();
            WordsEntityPersistor persistor = new WordsEntityPersistor(context.Object);
            persistor.Persist(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_LocationLocationsParameterIsNull()
        {
            var context = new Mock<IObjectContextAdapter>();
            LocationPersistor persistor = new LocationPersistor(context.Object);
            persistor.Persist(null);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_InproperContext()
        {
            var context = new Mock<IObjectContextAdapter>();
            TextFilesPersistor persistor = new TextFilesPersistor(context.Object, null);
        } 
    }
}
