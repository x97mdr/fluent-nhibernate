using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using System.Diagnostics;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class IdentityPart<T>
    {
        readonly IMappingStructure<IdMapping> structure;
        readonly IMappingStructure<GeneratorMapping> generatorStructure;
        readonly AccessStrategyBuilder<IdentityPart<T>> access;
        bool nextBool = true;

        public IdentityPart(IMappingStructure<IdMapping> structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<IdentityPart<T>>(this, value => structure.SetValue(Attr.Access, value));
            generatorStructure = new TypeStructure<GeneratorMapping>(typeof(T));
            structure.AddChild(generatorStructure);
        }

        public IdentityGenerationStrategyBuilder<IdentityPart<T>> GeneratedBy
        {
            get
            {
                return new IdentityGenerationStrategyBuilder<IdentityPart<T>>(generatorStructure, this, typeof(T));
            }
        }

        /// <summary>
        /// Set the access and naming strategy for this identity.
        /// </summary>
        public AccessStrategyBuilder<IdentityPart<T>> Access
        {
            get { return access; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IdentityPart<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        /// <summary>
        /// Sets the unsaved-value of the identity.
        /// </summary>
        /// <param name="unsavedValue">Value that represents an unsaved value.</param>
        public IdentityPart<T> UnsavedValue(object unsavedValue)
        {
            structure.SetValue(Attr.UnsavedValue, (unsavedValue ?? "null").ToString());
            return this;
        }

        /// <summary>
        /// Sets the column name for the identity field.
        /// </summary>
        /// <param name="columnName">Column name</param>
        public IdentityPart<T> Column(string columnName)
        {
            structure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);

            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(columnName);

            structure.AddChild(column);

            return this;
        }

        public IdentityPart<T> Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public IdentityPart<T> Precision(int precision)
        {
            structure.SetValue(Attr.Precision, precision);
            return this;
        }

        public IdentityPart<T> Scale(int scale)
        {
            structure.SetValue(Attr.Scale, scale);
            return this;
        }

        public IdentityPart<T> Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public IdentityPart<T> Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public IdentityPart<T> UniqueKey(string keyColumns)
        {
            structure.SetValue(Attr.UniqueKey, keyColumns);
            return this;
        }

        public IdentityPart<T> CustomSqlType(string sqlType)
        {
            structure.SetValue(Attr.SqlType, sqlType);
            return this;
        }

        public IdentityPart<T> Index(string key)
        {
            structure.SetValue(Attr.Index, key);
            return this;
        }

        public IdentityPart<T> Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
            return this;
        }

        public IdentityPart<T> Default(object value)
        {
            structure.SetValue(Attr.Default, value.ToString());
            return this;
        }

        public IdentityPart<T> CustomType<TCustomType>()
        {
            structure.SetValue(Attr.Type, new TypeReference(typeof(TCustomType)));
            return this;
        }

        public IdentityPart<T> CustomType(Type type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public IdentityPart<T> CustomType(string type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }
    }
}