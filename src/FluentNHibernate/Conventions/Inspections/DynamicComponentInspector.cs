using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Inspections
{
    public class DynamicComponentInspector : ComponentBaseInspector, IDynamicComponentInspector
    {
        private readonly ComponentMapping mapping;

        public DynamicComponentInspector(ComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public override bool IsSet(Attr property)
        {
            return mapping.HasUserDefinedValue(property);
        }
    }
}