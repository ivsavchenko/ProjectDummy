using System.Collections.Generic;

namespace TrinetixInterview.Contracts
{
    public interface IStringParser
    {
        IEnumerable<string> Parse(string input);
    }
}
