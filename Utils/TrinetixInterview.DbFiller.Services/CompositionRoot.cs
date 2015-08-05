using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.Unity;
using TrinetixInterview.ApplicationServices;
using TrinetixInterview.Contracts;
using TrinetixInterview.Model;

namespace TrinetixInterview.DbFiller.Services
{
    public static class CompositionRoot
    {
        private static readonly UnityContainer _container;

        static CompositionRoot()
        {
            _container = new UnityContainer();
        }

        public static UnityContainer Container
        {
            get { return _container; }
        }

        public static void Register()
        {
            _container.RegisterType<IObjectContextAdapter, TrinetixInterviewEntities>(new PerResolveLifetimeManager());

            _container.RegisterType<IBrowserFilter, TextFilesFilter>(ApplicationStrings.TextFilesFilter);
            _container.RegisterType<IBrowserFilter, Utf8EncodingFilter>(ApplicationStrings.Utf8EncodingFilter);
            _container.RegisterType<IStringParser, SplitStringParser>();            

            _container.RegisterType<IContainerBrowser, DirectoryRecursiveBrowser>(ApplicationStrings.DirectoryRecursiveBrowser);
            _container.RegisterType<IContainerBrowser, FileLinesBrowser>(ApplicationStrings.FileLinesBrowser);
            _container.RegisterType<IUnnormalizedLocationProcessor, TextFilesProcessor>(
                new InjectionConstructor(
                    new ResolvedParameter<IStringParser>(),
                    new ResolvedParameter<IContainerBrowser>(ApplicationStrings.DirectoryRecursiveBrowser),
                    new ResolvedParameter<IContainerBrowser>(ApplicationStrings.FileLinesBrowser),
                    new ResolvedParameter<IBrowserFilter>(ApplicationStrings.TextFilesFilter),
                    new ResolvedParameter<IBrowserFilter>(ApplicationStrings.Utf8EncodingFilter)));

            // the order of IEntityPersistor is important here the key entity (LocationPersistor) should be registered at the end
            _container.RegisterType<IEntityPersistor, WordsEntityPersistor>(ApplicationStrings.WordsEntityPersistor);
            _container.RegisterType<IEntityPersistor, FilePathesEntityPersistor>(ApplicationStrings.FilePathesEntityPersistor);
            _container.RegisterType<IEntityPersistor, FileNamesEntityPersistor>(ApplicationStrings.FileNamesEntityPersistor);
            _container.RegisterType<IEntityPersistor, LocationPersistor>(ApplicationStrings.LocationPersistor);

            _container.RegisterType<IEnumerable<IEntityPersistor>, IEntityPersistor[]>();

            _container.RegisterType<IIUnnormalizedLocationPersistor, TextFilesPersistor>();
        }
    }
}
