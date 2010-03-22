using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Instances
{
    public class MapInstance : MapInspector, IMapInstance
    {
        private readonly CollectionMapping mapping;
        public MapInstance(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        new public IIndexInstanceBase Index
        {
            get
            {
                if (mapping.Index is IndexMapping)
                { return new IndexInstance(mapping.Index as IndexMapping); }
                if (mapping.Index is IndexManyToManyMapping)
                { return new IndexManyToManyInstance(mapping.Index as IndexManyToManyMapping); }

                throw new InvalidOperationException("IIndexMapping is not a valid type for building an Index Instance ");
            }
        }

        public new void OrderBy(string orderBy)
        {
            if (mapping.HasUserDefinedValue(Attr.OrderBy))
                return;

            mapping.OrderBy = orderBy;
        }

        public new void Sort(string sort)
        {
            if (mapping.HasUserDefinedValue(Attr.Sort))
                return;

            mapping.Sort = sort;
        }

        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Access))
                        mapping.Access = value;
                });
            }
        }
    }
}
