using System;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Conventions.Instances
{
    public class SetInstance : SetInspector, ISetInstance
    {
        private readonly CollectionMapping mapping;
        public SetInstance(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new void OrderBy(string orderBy)
        {
            if (mapping.HasUserDefinedValue(Attr.OrderBy))
                return;

            mapping.OrderBy = orderBy;
        }

        public new void Sort(string sort)
        {
            if (mapping.HasUserDefinedValue(Attr.Sort))
                return;

            mapping.Sort = sort;
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

        public void OverrideInferredChildType(Type type)
        {
            mapping.ChildType = type;
        }
    }
}
