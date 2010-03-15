using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class CachePart
    {
        readonly IMappingStructure<CacheMapping> structure;

        public CachePart(IMappingStructure<CacheMapping> structure)
        {
            this.structure = structure;
        }

        public CachePart ReadWrite()
        {
            structure.SetValue(Attr.Usage, "read-write");
            return this;
        }

        public CachePart NonStrictReadWrite()
        {
            structure.SetValue(Attr.Usage, "nonstrict-read-write");
            return this;
        }

        public CachePart ReadOnly()
        {
            structure.SetValue(Attr.Usage, "read-only");
            return this;
        }

        public CachePart Transactional()
        {
            structure.SetValue(Attr.Usage, "transactional");
            return this;
        }

        public CachePart CustomUsage(string custom)
        {
            structure.SetValue(Attr.Usage, custom);
            return this;
        }

        public CachePart Region(string name)
        {
            structure.SetValue(Attr.Region, name);
            return this;
        }

        public CachePart IncludeAll()
        {
            structure.SetValue(Attr.Include, "all");
            return this;
        }

        public CachePart IncludeNonLazy()
        {
            structure.SetValue(Attr.Include, "non-lazy");
            return this;
        }

        public CachePart CustomInclude(string custom)
        {
            structure.SetValue(Attr.Include, custom);
            return this;
        }
    }
}