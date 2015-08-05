using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class Utf8EncodingFilter : IBrowserFilter
    {
        public string Pattern
        {
            get { return "UTF-8"; }
        }
    }
}
