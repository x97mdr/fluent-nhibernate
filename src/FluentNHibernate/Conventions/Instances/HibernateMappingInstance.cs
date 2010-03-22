using System;
using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class HibernateMappingInstance : HibernateMappingInspector, IHibernateMappingInstance
    {
        private readonly HibernateMapping mapping;
        private bool nextBool = true;

        public HibernateMappingInstance(HibernateMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new void Catalog(string catalog)
        {
            if (!mapping.HasUserDefinedValue(Attr.Catalog))
                mapping.Catalog = catalog;
        }

        public new void Schema(string schema)
        {
            if (!mapping.HasUserDefinedValue(Attr.Schema))
                mapping.Schema = schema;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IHibernateMappingInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new void DefaultLazy()
        {
            if (!mapping.HasUserDefinedValue(Attr.DefaultLazy))
                mapping.DefaultLazy = nextBool;
            nextBool = true;
        }

        public new void AutoImport()
        {
            if (!mapping.HasUserDefinedValue(Attr.AutoImport))
                mapping.AutoImport = nextBool;
            nextBool = true;
        }

        public new ICascadeInstance DefaultCascade
        {
            get
            {
                return new CascadeInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.DefaultCascade))
                        mapping.DefaultCascade = value;
                });
            }
        }

        public new IAccessInstance DefaultAccess
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.DefaultAccess))
                        mapping.DefaultAccess = value;
                });
            }
        }
    }
}