using System;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions.Instances
{
    internal class ColumnInstanceHelper
    {
        readonly ColumnBasedMappingBase mapping;

        public ColumnInstanceHelper(ColumnBasedMappingBase mapping)
        {
            this.mapping = mapping;
        }

        public bool ThisOrColumnHasUserDefinedValue(Attr attr)
        {
            return mapping.HasUserDefinedValue(attr) ||
                mapping.Columns.Any(x => x.HasUserDefinedValue(attr));
        }

        public void SetOnEachColumn(Action<ColumnMapping> set)
        {
            mapping.Columns.Each(set);
        }
    }
}