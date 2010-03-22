using System;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Instances
{
    public class SubclassInstance : SubclassInspector, ISubclassInstance
    {
        private readonly SubclassMapping mapping;
        private bool nextBool = true;

        public SubclassInstance(SubclassMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ISubclassInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new void DiscriminatorValue(object value)
        {
            if (!mapping.HasUserDefinedValue(Attr.DiscriminatorValue))
                mapping.DiscriminatorValue = value;
        }

        public new void Abstract()
        {
            if (!mapping.HasUserDefinedValue(Attr.Abstract))
                mapping.Abstract = nextBool;
            nextBool = true;
        }

        public new void DynamicInsert()
        {
            if (!mapping.HasUserDefinedValue(Attr.DynamicInsert))
                mapping.DynamicInsert = nextBool;
            nextBool = true;
        }

        public new void DynamicUpdate()
        {
            if (!mapping.HasUserDefinedValue(Attr.DynamicUpdate))
                mapping.DynamicUpdate = nextBool;
            nextBool = true;
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void Proxy(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = type.AssemblyQualifiedName;
        }

        public new void Proxy<T>()
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = typeof(T).AssemblyQualifiedName;
        }

        public new void SelectBeforeUpdate()
        {
            if (!mapping.HasUserDefinedValue(Attr.SelectBeforeUpdate))
                mapping.SelectBeforeUpdate = nextBool;
            nextBool = true;
        }
    }
}