using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;
using TrinetixInterview.Model;

namespace TrinetixInterview.ApplicationServices
{
    public class FilePathesEntityPersistor : IEntityPersistor
    {
        private readonly IObjectContextAdapter _context;

        public FilePathesEntityPersistor(IObjectContextAdapter context)
        {
            _context = context;
        }

        public void Persist(IEnumerable<UnnormalizedLocation> locations)
        {
            if (locations == null)
            {
                throw new ArgumentException("locations should not be null");
            }

            var groupedLocationByFilePath = locations.GroupBy(x => x.FilePath);

            foreach (IGrouping<string, UnnormalizedLocation> grouping in groupedLocationByFilePath)
            {
                PersistGroup(grouping);
            }
        }

        public virtual void PersistGroup(IGrouping<string, UnnormalizedLocation> groupedLocationByFilePath)
        {
            FilePath addedFilePath = _context.ObjectContext.CreateObject<FilePath>();
            addedFilePath.FullPath = groupedLocationByFilePath.Key;
            _context.ObjectContext.AddObject("FilePaths", addedFilePath);

            foreach (UnnormalizedLocation unnormalizedLocation in groupedLocationByFilePath)
            {
                unnormalizedLocation.FilePathId = addedFilePath;
            }
        }
    }
}
