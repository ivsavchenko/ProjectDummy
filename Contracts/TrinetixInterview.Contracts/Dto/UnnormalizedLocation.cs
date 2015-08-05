namespace TrinetixInterview.Contracts.Dto
{
    public class UnnormalizedLocation
    {
        public IKey FileNameId { get; set; }

        public IKey FilePathId { get; set; }

        public IKey WordId { get; set; }        

        public string FileName { get; set; }
        
        public string FilePath { get; set; }
        
        public string Word { get; set; }

        public long Row { get; set; }

        public long Column { get; set; }
    }
}
