using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.Sources;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;
using NHibernate.Cfg;

namespace FluentNHibernate
{
    public interface IMappingSource
    {
        IEnumerable<IMappingResult> GetResults();
    }

    public class FluentMappingSource : IMappingSource
    {
        readonly ITypeSource source;

        public FluentMappingSource(ITypeSource source)
        {
            this.source = new FluentTypeSource(source);
        }

        public IEnumerable<IMappingResult> GetResults()
        {
            return source.GetTypes()
                .Select(x => x.InstantiateUsingParameterlessConstructor<IMappingProvider>())
                .Select(x => x.GetClassMapping())
                .ToArray();
        }
    }

    public class PersistenceModel
    {
        readonly IAutomappingStrategy strategy;
        private readonly IList<IMappingModelVisitor> visitors = new List<IMappingModelVisitor>();
        public IConventionFinder Conventions { get; private set; }
        public bool MergeMappings { get; set; }
        private IEnumerable<HibernateMapping> compiledMappings;
        private readonly ValidationVisitor validationVisitor;
        readonly List<IMappingSource> sources = new List<IMappingSource>();

        public PersistenceModel(IAutomappingStrategy strategy, IConventionFinder conventionFinder)
        {
            this.strategy = strategy;
            Conventions = conventionFinder;

            //visitors.Add(new ComponentReferenceResolutionVisitor(componentProviders));
            visitors.Add(new ComponentColumnPrefixVisitor());
            //visitors.Add(new SeparateSubclassVisitor(subclassProviders));
            visitors.Add(new BiDirectionalManyToManyPairingVisitor());
            visitors.Add(new ManyToManyTableNameVisitor());
            visitors.Add(new ConventionVisitor(Conventions));
            visitors.Add((validationVisitor = new ValidationVisitor()));
        }

        public PersistenceModel()
            : this(new DefaultAutomappingStrategy(), new DefaultConventionFinder())
        {}

        protected void AddMappingsFromThisAssembly()
        {
            var assembly = FindTheCallingAssembly();
            AddMappingsFromAssembly(assembly);
        }

        public void AddMappingsFromAssembly(Assembly assembly)
        {
            AddMappingsSource(new AssemblyTypeSource(assembly));
        }

        public void AddMappingsSource(IMappingSource source)
        {
            sources.Add(source);
        }

        public void AddMappingsSource(ITypeSource source)
        {
            AddMappingsSource(new FluentMappingSource(source));
            //source.GetTypes()
            //    .Where(x => IsMappingOf<IMappingProvider>(x) ||
            //                IsMappingOf<IIndeterminateSubclassMappingProvider>(x) ||
            //                IsMappingOf<IExternalComponentMappingProvider>(x) ||
            //                IsMappingOf<IFilterDefinition>(x))
            //    .Each(Add);
        }

        private static Assembly FindTheCallingAssembly()
        {
            StackTrace trace = new StackTrace(Thread.CurrentThread, false);

            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            Assembly callingAssembly = null;
            for (int i = 0; i < trace.FrameCount; i++)
            {
                StackFrame frame = trace.GetFrame(i);
                Assembly assembly = frame.GetMethod().DeclaringType.Assembly;
                if (assembly != thisAssembly)
                {
                    callingAssembly = assembly;
                    break;
                }
            }
            return callingAssembly;
        }

        private bool IsMappingOf<T>(Type type)
        {
            return !type.IsGenericType && typeof(T).IsAssignableFrom(type);
        }

        public virtual IEnumerable<HibernateMapping> BuildMappings()
        {
            var results = sources.SelectMany(x => x.GetResults());
            var mappings = new List<ClassMapping>();

            foreach (var result in results.Where(x => x.RequiresAutomapping))
            {
                // TODO: Rewrite automapper
                //var automapper = new AutoMapper(Conventions, new InlineOverride[0], result.AutomappingStrategy ?? strategy);

                //automapper.Map(result.)
            }

            foreach (var result in results)
            {
                // TODO: add mapping
            }

            return null;
        }

        ClassMapping CreateMappingFromClassMap(IMappingProvider classMap)
        {
            var mapping = new ClassMapping();
            var mappingResult = classMap.GetClassMapping();

            mappingResult.ApplyTo(mapping);
            return mapping;
        }

        private void ApplyVisitors(IEnumerable<HibernateMapping> mappings)
        {
            foreach (var visitor in visitors)
                foreach (var mapping in mappings)
                    mapping.AcceptVisitor(visitor);
        }

        private void EnsureMappingsBuilt()
        {
            if (compiledMappings != null) return;

            compiledMappings = BuildMappings();
        }

        protected virtual string GetMappingFileName()
        {
            return "FluentMappings.hbm.xml";
        }

        public void WriteMappingsTo(string folder)
        {
            EnsureMappingsBuilt();

            foreach (var mapping in compiledMappings)
            {
                var serializer = new MappingXmlSerializer();
                var document = serializer.Serialize(mapping);
                string filename;
                if (MergeMappings)
                {
                    filename = GetMappingFileName();
                }
                else if (mapping.Classes.Count() > 0)
                {
                    filename = mapping.Classes.First().Type.FullName + ".hbm.xml";
                }
                else
                {
                    filename = "filter-def." + mapping.Filters.First().Name + ".hbm.xml";
                }

                using (var writer = new XmlTextWriter(Path.Combine(folder, filename), Encoding.Default))
                {
                    writer.Formatting = Formatting.Indented;
                    document.WriteTo(writer);
                }    
            }
        }

        public virtual void Configure(Configuration cfg)
        {
            EnsureMappingsBuilt();

            foreach (var mapping in compiledMappings.Where(m => m.Classes.Count() == 0))
            {
                var serializer = new MappingXmlSerializer();
                XmlDocument document = serializer.Serialize(mapping);
                cfg.AddDocument(document);
            }

            foreach (var mapping in compiledMappings.Where(m => m.Classes.Count() > 0))
            {
                var serializer = new MappingXmlSerializer();
                XmlDocument document = serializer.Serialize(mapping);

                if (cfg.GetClassMapping(mapping.Classes.First().Type) == null)
                    cfg.AddDocument(document);
            }
        }

        public bool ContainsMapping(Type type)
        {
            //return classProviders.Any(x => x.GetType() == type) ||
            //    filterDefinitions.Any(x => x.GetType() == type) ||
            //    subclassProviders.Any(x => x.GetType() == type) ||
            //    componentProviders.Any(x => x.GetType() == type);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets whether validation of mappings is performed. 
        /// </summary>
        public bool ValidationEnabled
        {
            get { return validationVisitor.Enabled; }
            set { validationVisitor.Enabled = value; }
        }
    }

    public interface IMappingProvider
    {
        Type Type { get; }
        IMappingResult GetClassMapping();
        // HACK: In place just to keep compatibility until verdict is made
    }

    //public class PassThroughMappingProvider : IMappingProvider
    //{
    //    private readonly ClassMapping mapping;

    //    public PassThroughMappingProvider(ClassMapping mapping)
    //    {
    //        this.mapping = mapping;
    //    }

    //    public ClassMapping GetClassMapping()
    //    {
    //        return mapping;
    //    }

    //    public HibernateMapping GetHibernateMapping()
    //    {
    //        return new HibernateMapping();
    //    }

    //    public IEnumerable<string> GetIgnoredProperties()
    //    {
    //        return new string[0];
    //    }
    //}
}