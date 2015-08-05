using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using TrinetixInterview.Contracts;
using TrinetixInterview.Contracts.Dto;
using TrinetixInterview.Model;

namespace TrinetixInterview.ApplicationServices
{
    public class TextFilesPersistor : IIUnnormalizedLocationPersistor
    {
        private readonly IEnumerable<IEntityPersistor> _entityPersistors;
        private readonly TrinetixInterviewEntities _context;
        
        public TextFilesPersistor(IObjectContextAdapter context, IEnumerable<IEntityPersistor> entityPersistors)
        {
            _context = context as TrinetixInterviewEntities;

            if (_context == null)
            {
                throw new ArgumentException("DbContext has inproper type");
            }

            // _context.Database.Log = Console.Write; 
            _context.Configuration.AutoDetectChangesEnabled = false;
            _entityPersistors = entityPersistors;
        }

        public void Persist(IEnumerable<UnnormalizedLocation> locations)
        {
            using (_context)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (IEntityPersistor entityPersistor in _entityPersistors)
                    {                        
                        entityPersistor.Persist(locations);

                        _context.ChangeTracker.DetectChanges();
                        _context.SaveChanges();
                    }

                    transaction.Commit();                   
                }
            }
        }
    }
}
