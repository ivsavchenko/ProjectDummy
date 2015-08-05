using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;
using TrinetixInterview.Model;

namespace TrinetixInterview.ApplicationServices
{
    public class LocationPersistor : IEntityPersistor
    {
        private readonly IObjectContextAdapter _context;

        public LocationPersistor(IObjectContextAdapter context)
        {
            _context = context;
        }

        public void Persist(IEnumerable<UnnormalizedLocation> locations)
        {
            if (locations == null)
            {
                throw new ArgumentException("locations should not be null");
            }

            foreach (UnnormalizedLocation unnormalizedLocation in locations)
            {
                Location addedLocation = _context.ObjectContext.CreateObject<Location>();
                addedLocation.Column = unnormalizedLocation.Column;
                addedLocation.FileNameId = unnormalizedLocation.FileNameId.Id;
                addedLocation.FilePathId = unnormalizedLocation.FilePathId.Id;
                addedLocation.Row = unnormalizedLocation.Row;
                addedLocation.WordId = unnormalizedLocation.WordId.Id;

                _context.ObjectContext.AddObject("Locations", addedLocation);
            }
        }

        public void PersistGroup(IGrouping<string, UnnormalizedLocation> groupedLocationByWord)
        {
            throw new InvalidOperationException();
        }
    }
}
