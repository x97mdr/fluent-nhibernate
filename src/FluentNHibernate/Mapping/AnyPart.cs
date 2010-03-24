using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Represents the "Any" mapping in NHibernate. It is impossible to specify a foreign key constraint for this kind of association. For more information
    /// please reference chapter 5.2.4 in the NHibernate online documentation
    /// </summary>
    public class AnyPart<T>
    {
        readonly IMappingStructure<AnyMapping> structure;
        private readonly AccessStrategyBuilder<AnyPart<T>> access;
        private readonly CascadeExpression<AnyPart<T>> cascade;
        private bool nextBool = true;

        public AnyPart(IMappingStructure<AnyMapping> structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<AnyPart<T>>(this, value => structure.SetValue(Attr.Access, value));
            cascade = new CascadeExpression<AnyPart<T>>(this, value => structure.SetValue(Attr.Cascade, value));
        }

        /// <summary>
        /// Defines how NHibernate will access the object for persisting/hydrating (Defaults to Property)
        /// </summary>
        public AccessStrategyBuilder<AnyPart<T>> Access
        {
            get { return access; }
        }

        /// <summary>
        /// Cascade style (Defaults to none)
        /// </summary>
        public CascadeExpression<AnyPart<T>> Cascade
        {
            get { return cascade; }
        }

        public AnyPart<T> IdentityType(Expression<Func<T, object>> expression)
        {
            return IdentityType(expression.ToMember().PropertyType);
        }

        public AnyPart<T> IdentityType<TIdentity>()
        {
            return IdentityType(typeof(TIdentity));
        }

        public AnyPart<T> IdentityType(Type type)
        {
            structure.SetValue(Attr.IdType, type.AssemblyQualifiedName);
            return this;
        }

        public AnyPart<T> EntityTypeColumn(string columnName)
        {
            var column = new ColumnStructure(structure);
            var part = new ColumnPart(column);
            part.Name(columnName);
            structure.AddChild(column);
            return this;
        }

        public AnyPart<T> EntityIdentifierColumn(string columnName)
        {
            var column = new IdentifierColumnStructure(structure);
            var part = new ColumnPart(column);
            part.Name(columnName);
            structure.AddChild(column);
            return this;
        }

        public AnyPart<T> AddMetaValue<TModel>(string valueMap)
        {
            structure.SetValue(Attr.MetaType, new TypeReference(typeof(string)));
            var metaValue = new TypeStructure<MetaValueMapping>(typeof(TModel));
            metaValue.SetValue(Attr.Value, valueMap);
            structure.AddChild(metaValue);
            return this;
        }

        public AnyPart<T> Insert()
        {
            structure.SetValue(Attr.Insert, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> Update()
        {
            structure.SetValue(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> ReadOnly()
        {
            structure.SetValue(Attr.Insert, !nextBool);
            structure.SetValue(Attr.Update, !nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public AnyPart<T> OptimisticLock()
        {
            structure.SetValue(Attr.OptimisticLock, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public AnyPart<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
