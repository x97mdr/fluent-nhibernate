﻿using System;
using System.Reflection;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class MapInspector : CollectionInspector, IMapInspector
    {
        private readonly InspectorModelMapper<IMapInspector, CollectionMapping> mappedProperties = new InspectorModelMapper<IMapInspector, CollectionMapping>();
        private readonly CollectionMapping mapping;

        public MapInspector(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            mappedProperties.Map(x => x.LazyLoad, x => x.Lazy);
        }

        public new bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
        }

        public IIndexInspectorBase Index
        {
            get
            {
                if (mapping.Index == null)
                    return new IndexInspector(new IndexMapping());

                if (mapping.Index is IndexMapping)
                    return new IndexInspector(mapping.Index as IndexMapping);

                if (mapping.Index is IndexManyToManyMapping)
                    return new IndexManyToManyInspector(mapping.Index as IndexManyToManyMapping);

                throw new InvalidOperationException("This IIndexMapping is not a valid type for inspecting");
            }
        }
        public new string OrderBy
        {
            get { return mapping.OrderBy; }
        }
        public string Sort
        {
            get { return mapping.Sort; }
        }
    }
}
