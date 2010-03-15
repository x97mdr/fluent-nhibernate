using System;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class SubClassPart<TSubclass> : ClasslikeMapBase<TSubclass>
    {
        readonly DiscriminatorPart parent;
        readonly IMappingStructure<SubclassMapping> structure;
        bool nextBool = true;

        public SubClassPart(DiscriminatorPart parent, IMappingStructure<SubclassMapping> structure)
            : base(structure)
        {
            this.parent = parent;
            this.structure = structure;
        }

        public DiscriminatorPart SubClass<TChild>(object discriminatorValue, Action<SubClassPart<TChild>> action)
        {
            var subclassStructure = new SubclassStructure(SubclassType.Subclass, typeof(TChild));
            var subclass = new SubClassPart<TChild>(parent, subclassStructure);

            if (discriminatorValue != null)
                subclass.DiscriminatorValue(discriminatorValue);

            action(subclass);

            structure.AddChild(subclassStructure);

            return parent;
        }

        public DiscriminatorPart SubClass<TChild>(Action<SubClassPart<TChild>> action)
        {
            return SubClass(null, action);
        }

        /// <summary>
        /// Sets whether this subclass is lazy loaded
        /// </summary>
        /// <returns></returns>
        public SubClassPart<TSubclass> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public SubClassPart<TSubclass> Proxy(Type type)
        {
            structure.SetValue(Attr.Proxy, type.AssemblyQualifiedName);
            return this;
        }

        public SubClassPart<TSubclass> Proxy<T>()
        {
            return Proxy(typeof(T));
        }

        public SubClassPart<TSubclass> DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public SubClassPart<TSubclass> DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
            return this;
        }

        public SubClassPart<TSubclass> SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public SubClassPart<TSubclass> Abstract()
        {
            structure.SetValue(Attr.Abstract, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public void EntityName(string entityName)
        {
            structure.SetValue(Attr.EntityName, entityName);
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SubClassPart<TSubclass> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public SubClassPart<TSubclass> DiscriminatorValue(object value)
        {
            structure.SetValue(Attr.DiscriminatorValue, value);
            return this;
        }
    }
}
