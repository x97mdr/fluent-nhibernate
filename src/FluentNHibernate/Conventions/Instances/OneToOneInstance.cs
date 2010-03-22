using System;
using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class OneToOneInstance : OneToOneInspector, IOneToOneInstance
    {
        private readonly OneToOneMapping mapping;
        private bool nextBool = true;

        public OneToOneInstance(OneToOneMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
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

        public new ICascadeInstance Cascade
        {
            get
            {
                return new CascadeInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Cascade))
                        mapping.Cascade = value;
                });
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IOneToOneInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new IFetchInstance Fetch
        {
            get
            {
                return new FetchInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Fetch))
                        mapping.Fetch = value;
                });
            }
        }

        public new void Class<T>()
        {
            if (!mapping.HasUserDefinedValue(Attr.Class))
                mapping.Class = new TypeReference(typeof(T));
        }

        public new void Class(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Class))
                mapping.Class = new TypeReference(type);
        }

        public new void Constrained()
        {
            if (!mapping.HasUserDefinedValue(Attr.Constrained))
                mapping.Constrained = nextBool;
            nextBool = true;
        }

        public new void ForeignKey(string key)
        {
            if (!mapping.HasUserDefinedValue(Attr.ForeignKey))
                mapping.ForeignKey = key;
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void PropertyRef(string propertyName)
        {
            if (!mapping.HasUserDefinedValue(Attr.PropertyRef))
                mapping.PropertyRef = propertyName;
        }

        public void OverrideInferredClass(Type type)
        {
            mapping.Class = new TypeReference(type);
        }
    }
}