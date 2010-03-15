using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class DynamicComponentPart<T> : ComponentPartBase<T>
    {
        readonly IMappingStructure<ComponentMapping> structure;
        private readonly AccessStrategyBuilder<DynamicComponentPart<T>> access;

        public DynamicComponentPart(IMappingStructure<ComponentMapping> structure)
            : base(structure)
        {
            this.structure = structure;
            access = new AccessStrategyBuilder<DynamicComponentPart<T>>(this, value => structure.SetValue(Attr.Access, value));
        }

        /// <summary>
        /// Set the access and naming strategy for this component.
        /// </summary>
        public new AccessStrategyBuilder<DynamicComponentPart<T>> Access
        {
            get { return access; }
        }

        public new DynamicComponentPart<T> ParentReference(Expression<Func<T, object>> exp)
        {
            base.ParentReference(exp);
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public new DynamicComponentPart<T> Not
        {
            get
            {
                var forceExecution = base.Not;
                return this;
            }
        }

        public new DynamicComponentPart<T> Unique()
        {
            base.Unique();
            return this;
        }

        public new DynamicComponentPart<T> ReadOnly()
        {
            base.ReadOnly();
            return this;
        }

        public new DynamicComponentPart<T> Insert()
        {
            base.Insert();
            return this;
        }

        public new DynamicComponentPart<T> Update()
        {
            base.Update();
            return this;
        }

        public new DynamicComponentPart<T> OptimisticLock()
        {
            base.OptimisticLock();
            return this;
        }

        public PropertyPart Map(string key)
        {
            return Map<string>(key);
        }

        public PropertyPart Map<TProperty>(string key)
        {
            var propertyStructure = new MemberStructure<PropertyMapping>(new DummyPropertyInfo(key, typeof(TProperty)).ToMember());
            var propertyMap = new PropertyPart(propertyStructure);

            structure.AddChild(propertyStructure);

            return propertyMap;
        }
    }
}