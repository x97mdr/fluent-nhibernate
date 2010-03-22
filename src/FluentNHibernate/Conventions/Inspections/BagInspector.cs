using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class BagInspector : CollectionInspector, IBagInspector
    {
        private readonly CollectionMapping mapping;

        public BagInspector(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new string OrderBy
        {
            get { return mapping.OrderBy; }
        }
    }
}
