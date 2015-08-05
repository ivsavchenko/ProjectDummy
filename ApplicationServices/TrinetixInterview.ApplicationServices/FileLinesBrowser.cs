using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class FileLinesBrowser : IContainerBrowser
    {
        public IEnumerable<string> Browse(string containerLocation, IBrowserFilter filter = null)
        {
            if (containerLocation == null)
            {
                throw new ArgumentException("containerLocation is null");
            }

            if (filter != null)
            {
                return File.ReadAllLines(containerLocation, Encoding.GetEncoding(filter.Pattern));
            }

            return File.ReadAllLines(containerLocation);
        }
    }
}
