using System;
using Microsoft.Practices.Unity;
using TrinetixInterview.Contracts;
using TrinetixInterview.DbFiller.Services;

namespace TrinetixInterview.DbFiller
{
    public class Program
    {
        public static void Main(string[] path)
        {
            if (path.Length != 1)
            {
                Console.WriteLine(@"Application requires exactly one parameter.");
                Console.WriteLine(@"For example TrinetixInterview.DbFiller.exe C:\FolderWithFiles\");
                return;
            }

            using (CompositionRoot.Container)
            {
                CompositionRoot.Register();

                IUnnormalizedLocationProcessor processor = CompositionRoot.Container.Resolve<IUnnormalizedLocationProcessor>();
                IIUnnormalizedLocationPersistor persistor = CompositionRoot.Container.Resolve<IIUnnormalizedLocationPersistor>();
                processor.PopulateUnnormalizedData(path[0]);

                DateTime start = DateTime.Now;
                Console.WriteLine("Start: {0}", start);

                persistor.Persist(processor.Data);

                DateTime end = DateTime.Now;
                Console.WriteLine("End: {0}", end);
                Console.WriteLine("It's taken: {0}", (end - start).TotalSeconds);
            }
        }
    }
}
