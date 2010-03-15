using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class JoinedSubClassPart<TSubclass> : ClasslikeMapBase<TSubclass>
    {
        readonly IMappingStructure<SubclassMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        readonly ColumnMappingCollection<JoinedSubClassPart<TSubclass>> columns;
        bool nextBool = true;

        public JoinedSubClassPart(IMappingStructure<SubclassMapping> structure)
            : base(structure)
        {
            this.structure = structure;
            keyStructure = new FreeStructure<KeyMapping>();
            structure.AddChild(keyStructure);
            columns = new ColumnMappingCollection<JoinedSubClassPart<TSubclass>>(this, keyStructure);
        }

        public virtual void JoinedSubClass<TNextSubclass>(string keyColumn, Action<JoinedSubClassPart<TNextSubclass>> action)
        {
            var subclassStructure = new SubclassStructure(SubclassType.JoinedSubclass, typeof(TNextSubclass));
            var subclass = new JoinedSubClassPart<TNextSubclass>(subclassStructure);

            subclass.KeyColumns.Add(keyColumn);

            action(subclass);

            structure.AddChild(subclassStructure);
        }

        public ColumnMappingCollection<JoinedSubClassPart<TSubclass>> KeyColumns
        {
            get { return columns; }
        }

        public JoinedSubClassPart<TSubclass> Table(string tableName)
        {
            structure.SetValue(Attr.Table, tableName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
            return this;
        }

        public JoinedSubClassPart<TSubclass> CheckConstraint(string constraintName)
        {
            structure.SetValue(Attr.Check, constraintName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Proxy(Type type)
        {
            structure.SetValue(Attr.Proxy, type.AssemblyQualifiedName);
            return this;
        }

        public JoinedSubClassPart<TSubclass> Proxy<T>()
        {
            return Proxy(typeof(T));
        }

        public JoinedSubClassPart<TSubclass> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
            return this;
        }

        public JoinedSubClassPart<TSubclass> Abstract()
        {
            structure.SetValue(Attr.Abstract, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public JoinedSubClassPart<TSubclass> EntityName(string entityName)
        {
            structure.SetValue(Attr.EntityName, entityName);
            return this;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public JoinedSubClassPart<TSubclass> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}