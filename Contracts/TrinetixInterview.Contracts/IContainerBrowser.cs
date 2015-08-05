using System.Collections.Generic;

namespace TrinetixInterview.Contracts
{
    public interface IContainerBrowser
    {
        IEnumerable<string> Browse(string containerLocation, IBrowserFilter filter = null);
    }
}
