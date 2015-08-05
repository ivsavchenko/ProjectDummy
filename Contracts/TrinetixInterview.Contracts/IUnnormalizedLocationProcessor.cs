using System.Collections.Concurrent;
using TrinetixInterview.Contracts.Dto;

namespace TrinetixInterview.Contracts
{
    public interface IUnnormalizedLocationProcessor
    {
        ConcurrentBag<UnnormalizedLocation> Data { get; }

        void PopulateUnnormalizedData(string pathToData);
    }
}
