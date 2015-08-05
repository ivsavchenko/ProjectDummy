using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;
using TrinetixInterview.Model;

namespace TrinetixInterview.ApplicationServices
{
    public class FileNamesEntityPersistor : IEntityPersistor
    {
        private readonly IObjectContextAdapter _context;

        public FileNamesEntityPersistor(IObjectContextAdapter context)
        {
            _context = context;
        }

        public void Persist(IEnumerable<UnnormalizedLocation> locations)
        {
            if (locations == null)
            {
                throw new ArgumentException("locations should not be null");
            }

            var groupedLocationByFileName = locations.GroupBy(x => x.FileName);
            
            foreach (IGrouping<string, UnnormalizedLocation> grouping in groupedLocationByFileName)
            {
                PersistGroup(grouping);
            }            
        }

        public virtual void PersistGroup(IGrouping<string, UnnormalizedLocation> groupedLocationByFileName)
        {
            FileName addedFileName = _context.ObjectContext.CreateObject<FileName>();
            addedFileName.Name = groupedLocationByFileName.Key;
            _context.ObjectContext.AddObject("FileNames", addedFileName);

            foreach (UnnormalizedLocation unnormalizedLocation in groupedLocationByFileName)
            {
                unnormalizedLocation.FileNameId = addedFileName;
            }
        }
    }
}
