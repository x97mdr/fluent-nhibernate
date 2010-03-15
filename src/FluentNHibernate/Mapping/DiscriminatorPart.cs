using System;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class DiscriminatorPart
    {
        readonly IMappingStructure<DiscriminatorMapping> structure;
        readonly IMappingStructure parentStructure;
        bool nextBool = true;

        public DiscriminatorPart(IMappingStructure<DiscriminatorMapping> structure, IMappingStructure parentStructure)
        {
            this.structure = structure;
            this.parentStructure = parentStructure;
        }

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public DiscriminatorPart SubClass<TSubClass>(object discriminatorValue, Action<SubClassPart<TSubClass>> action)
        {
            var subclassStructure = new SubclassStructure(SubclassType.Subclass, typeof(TSubClass));
            var subclass = new SubClassPart<TSubClass>(this, subclassStructure);

            if (discriminatorValue != null)
                subclass.DiscriminatorValue(discriminatorValue);

            action(subclass);
            parentStructure.AddChild(subclassStructure);

            return this;
        }

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public DiscriminatorPart SubClass<TSubClass>(Action<SubClassPart<TSubClass>> action)
        {
            return SubClass(null, action);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DiscriminatorPart Not
        {
             get
             {
                 nextBool = !nextBool;
                 return this;
             }
        }

        /// <summary>
        /// Force NHibernate to always select using the discriminator value, even when selecting all subclasses. This
        /// can be useful when your table contains more discriminator values than you have classes (legacy).
        /// </summary>
        /// <remarks>Sets the "force" attribute.</remarks>
        public DiscriminatorPart AlwaysSelectWithValue()
        {
            structure.SetValue(Attr.Force, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Set this discriminator as read-only. Call this if your discriminator column is also part of a mapped composite identifier.
        /// </summary>
        /// <returns>Sets the "insert" attribute.</returns>
        public DiscriminatorPart ReadOnly()
        {
            structure.SetValue(Attr.Insert, !nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// An arbitrary SQL expression that is executed when a type has to be evaluated. Allows content-based discrimination.
        /// </summary>
        /// <param name="sql">SQL expression</param>
        public DiscriminatorPart Formula(string sql)
        {
            structure.SetValue(Attr.Formula, sql);
            return this;
        }

        public DiscriminatorPart Precision(int precision)
        {
            structure.SetValue(Attr.Precision, precision);
            return this;
        }

        public DiscriminatorPart Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public DiscriminatorPart Scale(int scale)
        {
            structure.SetValue(Attr.Scale, scale);
            return this;
        }

        public DiscriminatorPart Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public DiscriminatorPart Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public DiscriminatorPart UniqueKey(string keyColumns)
        {
            structure.SetValue(Attr.UniqueKey, keyColumns);
            return this;
        }

        public DiscriminatorPart Index(string index)
        {
            structure.SetValue(Attr.Index, index);
            return this;
        }

        public DiscriminatorPart Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
            return this;
        }

        public DiscriminatorPart Default(object value)
        {
            structure.SetValue(Attr.Default, value.ToString());
            return this;
        }

        public DiscriminatorPart CustomType<T>()
        {
            structure.SetValue(Attr.Type, new TypeReference(typeof(T)));
            return this;
        }

        public DiscriminatorPart CustomType(Type type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public DiscriminatorPart CustomType(string type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public DiscriminatorPart Column(string columnName)
        {
            var columnStructure = new ColumnStructure(structure);
            var part = new ColumnPart(columnStructure);
            part.Name(columnName);
            structure.AddChild(columnStructure);
            return this;
        }
    }
}