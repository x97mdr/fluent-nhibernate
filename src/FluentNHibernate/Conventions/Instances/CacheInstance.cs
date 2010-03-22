using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class CacheInstance : CacheInspector, ICacheInstance
    {
        private readonly CacheMapping mapping;

        public CacheInstance(CacheMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public void ReadWrite()
        {
            if (!mapping.HasUserDefinedValue(Attr.Usage))
                mapping.Usage = "read-write";
        }

        public void NonStrictReadWrite()
        {
            if (!mapping.HasUserDefinedValue(Attr.Usage))
                mapping.Usage = "nonstrict-read-write";
        }

        public void ReadOnly()
        {
            if (!mapping.HasUserDefinedValue(Attr.Usage))
                mapping.Usage = "read-only";
        }

        public void Transactional()
        {
            if (!mapping.HasUserDefinedValue(Attr.Usage))
                mapping.Usage = "transactional";
        }

        public void IncludeAll()
        {
            if (!mapping.HasUserDefinedValue(Attr.Include))
                mapping.Include = "all";
        }

        public void IncludeNonLazy()
        {
            if (!mapping.HasUserDefinedValue(Attr.Include))
                mapping.Include = "non-lazy";
        }

        public void CustomInclude(string include)
        {
            if (!mapping.HasUserDefinedValue(Attr.Include))
                mapping.Include = include;
        }

        public void CustomUsage(string custom)
        {
            if (!mapping.HasUserDefinedValue(Attr.Usage))
                mapping.Usage = custom;
        }

        public new void Region(string name)
        {
            if (!mapping.HasUserDefinedValue(Attr.Region))
                mapping.Region = name;
        }
    }
}