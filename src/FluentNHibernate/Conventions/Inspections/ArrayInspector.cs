﻿using System;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class ArrayInspector : CollectionInspector, IArrayInspector
    {
        private readonly InspectorModelMapper<IArrayInspector, CollectionMapping> mappedProperties = new InspectorModelMapper<IArrayInspector, CollectionMapping>();
        private readonly CollectionMapping mapping;

        public ArrayInspector(CollectionMapping mapping)
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
    }
}
