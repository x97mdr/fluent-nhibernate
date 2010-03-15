using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class SetInspector : CollectionInspector, ISetInspector
    {
        private readonly InspectorModelMapper<ISetInspector, CollectionMapping> mappedProperties = new InspectorModelMapper<ISetInspector, CollectionMapping>();
        private readonly CollectionMapping mapping;

        public SetInspector(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            mappedProperties.Map(x => x.LazyLoad, x => x.Lazy);
        }

        public new bool IsSet(Member property)
        {
            return mapping.IsSpecified(mappedProperties.Get(property));
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
