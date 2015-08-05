using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;
using TrinetixInterview.Model;

namespace TrinetixInterview.ApplicationServices
{
    public class WordsEntityPersistor : IEntityPersistor
    {
        private readonly IObjectContextAdapter _context;

        public WordsEntityPersistor(IObjectContextAdapter context) 
        {
            _context = context;
        }

        public void Persist(IEnumerable<UnnormalizedLocation> locations)
        {
            if (locations == null)
            {
                throw new ArgumentException("locations should not be null");
            }

            var groupedLocationByWord = locations.GroupBy(x => x.Word);
            
            foreach (IGrouping<string, UnnormalizedLocation> grouping in groupedLocationByWord)
            {
                PersistGroup(grouping);
            }
        }

        public virtual void PersistGroup(IGrouping<string, UnnormalizedLocation> groupedLocationByWord)
        {
            Word addedWord = _context.ObjectContext.CreateObject<Word>(); 
            addedWord.Name = groupedLocationByWord.Key;
            _context.ObjectContext.AddObject("Words", addedWord);

            foreach (UnnormalizedLocation unnormalizedLocation in groupedLocationByWord)
            {
                unnormalizedLocation.WordId = addedWord;
            }
        }
    }
}
