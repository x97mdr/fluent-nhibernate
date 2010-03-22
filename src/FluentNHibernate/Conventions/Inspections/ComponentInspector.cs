using System;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Inspections
{
    public class ComponentInspector : ComponentBaseInspector, IComponentInspector
    {
        private readonly ComponentMapping mapping;

        public ComponentInspector(ComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public override bool IsSet(Attr property)
        {
            return mapping.HasUserDefinedValue(property);
        }

        public bool LazyLoad
        {
            get { return mapping.Lazy; }
        }
    }
}