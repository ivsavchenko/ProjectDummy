using System;
using System.Collections.Generic;
using System.IO;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class DirectoryRecursiveBrowser : IContainerBrowser
    {
        public IEnumerable<string> Browse(string containerLocation, IBrowserFilter filter = null)
        {
            if (containerLocation == null)
            {
                throw new ArgumentException("containerLocation is null");
            }

            return Directory.GetFiles(containerLocation, filter == null ? "*.*" : filter.Pattern, SearchOption.AllDirectories);
        }
    }
}
