using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class BagInspector : CollectionInspector, IBagInspector
    {
        private readonly InspectorModelMapper<IBagInspector, CollectionMapping> mappedProperties = new InspectorModelMapper<IBagInspector, CollectionMapping>();
        private readonly CollectionMapping mapping;

        public BagInspector(CollectionMapping mapping)
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
    }
}
