using TrinetixInterview.Contracts;

namespace TrinetixInterview.ApplicationServices
{
    public class TextFilesFilter : IBrowserFilter
    {
        public string Pattern
        {
            get { return "*.txt"; }
        }
    }
}
