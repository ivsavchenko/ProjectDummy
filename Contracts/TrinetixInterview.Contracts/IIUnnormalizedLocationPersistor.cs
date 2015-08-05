using System.Collections.Generic;
using TrinetixInterview.Contracts.Dto;

namespace TrinetixInterview.Contracts
{
    public interface IIUnnormalizedLocationPersistor
    {
        void Persist(IEnumerable<UnnormalizedLocation> unnormalizedLocation);
    }
}
