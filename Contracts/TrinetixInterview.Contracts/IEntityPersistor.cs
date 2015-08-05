using System.Collections.Generic;
using System.Linq;
using TrinetixInterview.Contracts.Dto;

namespace TrinetixInterview.Contracts
{
    public interface IEntityPersistor
    {
        void Persist(IEnumerable<UnnormalizedLocation> locations);

        void PersistGroup(IGrouping<string, UnnormalizedLocation> groupedLocationByWord);
    }
}
