using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Instances
{
    public class DynamicComponentInstance : DynamicComponentInspector, IDynamicComponentInstance
    {
        private readonly ComponentMapping mapping;
        private bool nextBool;

        public DynamicComponentInstance(ComponentMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            nextBool = true;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IDynamicComponentInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
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

        public new void Update()
        {
            if (!mapping.HasUserDefinedValue(Attr.Update))
                mapping.Update = nextBool;
            nextBool = true;
        }

        public new void Insert()
        {
            if (!mapping.HasUserDefinedValue(Attr.Insert))
                mapping.Insert = nextBool;
            nextBool = true;
        }

        public new void Unique()
        {
            if (!mapping.HasUserDefinedValue(Attr.Unique))
                mapping.Unique = nextBool;
            nextBool = true;
        }

        public new void OptimisticLock()
        {
            if (!mapping.HasUserDefinedValue(Attr.OptimisticLock))
                mapping.OptimisticLock = nextBool;
            nextBool = true;
        }
    }
}