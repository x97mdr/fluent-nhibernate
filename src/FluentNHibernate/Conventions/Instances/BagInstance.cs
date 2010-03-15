using System;
using FluentNHibernate.Conventions.Inspections;
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
            if (mapping.IsSpecified("OrderBy"))
                return;

            mapping.OrderBy = orderBy;
        }

        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.IsSpecified("Access"))
                        mapping.Access = value;
                });
            }
        }
    }
}
