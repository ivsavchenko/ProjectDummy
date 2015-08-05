using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class RegexStringParser : IStringParser
    {
        public IEnumerable<string> Parse(string input)
        {
            return Regex.Split(input, @"[^\p{L}]*\p{Z}[^\p{L}]*");
        }
    }
}
