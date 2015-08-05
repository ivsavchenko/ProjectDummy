using System.Collections.Generic;
using System.Linq;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class SplitStringParser : IStringParser
    {
        public IEnumerable<string> Parse(string input)
        {
            return
                input.Split(new[] { ' ', ',', '.', ':', ';', '!', '?', '/', '\t' })
                    .Except(new[] { " ", ",", ".", ":", ";", "!", "?", "\t", "-", "/", string.Empty });
        }
    }
}
