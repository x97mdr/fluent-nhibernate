using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Inspections
{
    public class SetInspector : CollectionInspector, ISetInspector
    {
        private readonly CollectionMapping mapping;

        public SetInspector(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
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
