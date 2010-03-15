using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class HibernateMappingPart : IHibernateMappingProvider
    {
        readonly IMappingStructure<HibernateMapping> structure;
        bool nextBool = true;

        public HibernateMappingPart(IMappingStructure<HibernateMapping> structure)
        {
            this.structure = structure;
        }

        public HibernateMappingPart Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
            return this;
        }

        public CascadeExpression<HibernateMappingPart> DefaultCascade
        {
            get { return new CascadeExpression<HibernateMappingPart>(this, value => structure.SetValue(Attr.DefaultCascade, value)); }
        }

        public AccessStrategyBuilder<HibernateMappingPart> DefaultAccess
        {
            get { return new AccessStrategyBuilder<HibernateMappingPart>(this, value => structure.SetValue(Attr.DefaultAccess, value)); }
        }

        public HibernateMappingPart AutoImport()
        {
            structure.SetValue(Attr.AutoImport, nextBool);
            nextBool = true;
            return this;
        }

        public HibernateMappingPart DefaultLazy()
        {
            structure.SetValue(Attr.DefaultLazy, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public HibernateMappingPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public HibernateMappingPart Catalog(string catalog)
        {
            structure.SetValue(Attr.Catalog, catalog);
            return this;
        }

        public HibernateMappingPart Namespace(string ns)
        {
            structure.SetValue(Attr.Namespace, ns);
            return this;
        }

        public HibernateMappingPart Assembly(string assembly)
        {
            structure.SetValue(Attr.Assembly, assembly);
            return this;
        }

        HibernateMapping IHibernateMappingProvider.GetHibernateMapping()
        {
            // TODO: Extract this stuff into the PersistenceModel
            var mapping = structure.CreateMappingNode();

            structure.ApplyCustomisations();

            return (HibernateMapping)mapping;
        }
    }
}