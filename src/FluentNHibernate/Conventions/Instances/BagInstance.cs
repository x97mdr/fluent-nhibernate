using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Instances
{
    public class BagInstance : BagInspector, IBagInstance
    {
        private readonly CollectionMapping mapping;
        public BagInstance(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public void SetOrderBy(string orderBy)
        {
            if (mapping.HasUserDefinedValue(Attr.OrderBy))
                return;

            mapping.OrderBy = orderBy;
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
