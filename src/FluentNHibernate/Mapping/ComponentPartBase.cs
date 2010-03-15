using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public abstract class ComponentPartBase<T> : ClasslikeMapBase<T>
    {
        readonly IMappingStructure<ComponentMapping> structure;
        readonly AccessStrategyBuilder<ComponentPartBase<T>> access;
        
        protected bool nextBool = true;

        protected ComponentPartBase(IMappingStructure<ComponentMapping> structure)
            : base(structure)
        {
            this.structure = structure;
            access = new AccessStrategyBuilder<ComponentPartBase<T>>(this, value => structure.SetValue(Attr.Access, value));
        }

        /// <summary>
        /// Set the access and naming strategy for this component.
        /// </summary>
        public AccessStrategyBuilder<ComponentPartBase<T>> Access
        {
            get { return access; }
        }

        public ComponentPartBase<T> ParentReference(Expression<Func<T, object>> expression)
        {
            return ParentReference(expression.ToMember());
        }

        private ComponentPartBase<T> ParentReference(Member property)
        {
            var parentStructure = new MemberStructure<ParentMapping>(property);
            
            structure.AddChild(parentStructure);

            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ComponentPartBase<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public ComponentPartBase<T> ReadOnly()
        {
            structure.SetValue(Attr.Insert, !nextBool);
            structure.SetValue(Attr.Update, !nextBool);
            nextBool = true;

            return this;
        }

        public ComponentPartBase<T> Insert()
        {
            structure.SetValue(Attr.Insert, nextBool);
            nextBool = true;
            return this;
        }

        public ComponentPartBase<T> Update()
        {
            structure.SetValue(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public ComponentPartBase<T> Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public ComponentPartBase<T> OptimisticLock()
        {
            structure.SetValue(Attr.OptimisticLock, nextBool);
            nextBool = true;
            return this;
        }
    }
}