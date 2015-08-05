using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;

namespace TrinetixInterview.ApplicationServices
{
    public class TextFilesProcessor : IUnnormalizedLocationProcessor
    {
        private readonly IStringParser _stringParser;
        private readonly IContainerBrowser _directoryRecursiveBrowser;
        private readonly IContainerBrowser _fileLinesBrowser;
        private readonly IBrowserFilter _directoryRecursiveBrowserFilter;
        private readonly IBrowserFilter _fileLinesBrowserFilter;        

        #region Ctors
        public TextFilesProcessor(IStringParser stringParser, IContainerBrowser directoryRecursiveBrowser, IContainerBrowser fileLinesBrowser)
        {
            Data = new ConcurrentBag<UnnormalizedLocation>();
            _stringParser = stringParser;
            _directoryRecursiveBrowser = directoryRecursiveBrowser;
            _fileLinesBrowser = fileLinesBrowser;
        }

        public TextFilesProcessor(IStringParser stringParser, IContainerBrowser directoryRecursiveBrowser, IContainerBrowser fileLinesBrowser, IBrowserFilter directoryRecursiveBrowserFilter = null)
            : this(stringParser, directoryRecursiveBrowser, fileLinesBrowser)
        {
            _directoryRecursiveBrowserFilter = directoryRecursiveBrowserFilter;
        }

        public TextFilesProcessor(IStringParser stringParser, IContainerBrowser directoryRecursiveBrowser, IContainerBrowser fileLinesBrowser, IBrowserFilter directoryRecursiveBrowserFilter = null, IBrowserFilter fileLinesBrowserFilter = null)
            : this(stringParser, directoryRecursiveBrowser, fileLinesBrowser, directoryRecursiveBrowserFilter)
        {
            _fileLinesBrowserFilter = fileLinesBrowserFilter;
        } 
        #endregion

        public ConcurrentBag<UnnormalizedLocation> Data { get; private set; }

        public virtual void PopulateUnnormalizedData(string pathToData)
        {
            IEnumerable<string> filePathes = GetFiles(pathToData, _directoryRecursiveBrowser, _directoryRecursiveBrowserFilter);

            Parallel.ForEach(filePathes, fullFileName =>
            {
                var linesInFile = _fileLinesBrowser.Browse(fullFileName, _fileLinesBrowserFilter).ToList();

                Parallel.For(0, linesInFile.Count, i =>
                {
                    string[] wordsInLine = _stringParser.Parse(linesInFile[i]).ToArray();

                    for (int j = 0; j < wordsInLine.Count(); j++)
                    {
                        Data.Add(new UnnormalizedLocation
                        {
                            FileName = Path.GetFileName(fullFileName),
                            FilePath = Path.GetDirectoryName(fullFileName),
                            Word = wordsInLine[j],
                            Row = i,
                            Column = j
                        });
                    }
                });
            });
        }

        internal virtual IEnumerable<string> GetFiles(string path, IContainerBrowser directoryRecursiveBrowser, IBrowserFilter filter = null)
        {
            return directoryRecursiveBrowser.Browse(path, filter);
        }
    }
}
