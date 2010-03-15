using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ComponentPart<T> : ComponentPartBase<T>
    {
        readonly IMappingStructure<ComponentMapping> structure;
        readonly AccessStrategyBuilder<ComponentPart<T>> access;

        public ComponentPart(IMappingStructure<ComponentMapping> structure)
            : base(structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<ComponentPart<T>>(this, value => structure.SetValue(Attr.Access, value));
        }

        /// <summary>
        /// Set the access and naming strategy for this component.
        /// </summary>
        public new AccessStrategyBuilder<ComponentPart<T>> Access
        {
            get { return access; }
        }

        public new ComponentPart<T> ParentReference(Expression<Func<T, object>> exp)
        {
            base.ParentReference(exp);
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public new ComponentPart<T> Not
        {
            get
            {
                var forceExecution = base.Not;
                return this;
            }
        }

        public new ComponentPart<T> ReadOnly()
        {
            base.ReadOnly();
            return this;
        }

        public new ComponentPart<T> Insert()
        {
            base.Insert();
            return this;
        }

        public new ComponentPart<T> Update()
        {
            base.Update();
            return this;
        }

        public ComponentPart<T> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public new ComponentPart<T> OptimisticLock()
        {
            base.OptimisticLock();
            return this;
        }
    }
}
