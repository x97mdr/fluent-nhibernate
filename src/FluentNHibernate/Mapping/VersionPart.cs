using System;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class VersionPart
    {
        readonly IMappingStructure<VersionMapping> structure;
        readonly AccessStrategyBuilder<VersionPart> access;
        readonly VersionGeneratedBuilder<VersionPart> generated;
        bool nextBool = true;

        public VersionPart(IMappingStructure<VersionMapping> structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<VersionPart>(this, value => structure.SetValue(Attr.Access, value));
            generated = new VersionGeneratedBuilder<VersionPart>(this, value => structure.SetValue(Attr.Generated, value));
        }

        public VersionGeneratedBuilder<VersionPart> Generated
        {
            get { return generated; }
        }

        public AccessStrategyBuilder<VersionPart> Access
        {
            get { return access; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public VersionPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public VersionPart Column(string name)
        {
            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(name);
            
            structure.AddChild(column);

            return this;
        }

        public VersionPart UnsavedValue(string value)
        {
            structure.SetValue(Attr.UnsavedValue, value);
            return this;
        }

        public VersionPart Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public VersionPart Precision(int precision)
        {
            structure.SetValue(Attr.Precision, precision);
            return this;
        }

        public VersionPart Scale(int scale)
        {
            structure.SetValue(Attr.Scale, scale);
            return this;
        }

        public VersionPart Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public VersionPart Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public VersionPart UniqueKey(string keyColumns)
        {
            structure.SetValue(Attr.UniqueKey, keyColumns);
            return this;
        }

        public VersionPart Index(string index)
        {
            structure.SetValue(Attr.Index, index);
            return this;
        }

        public VersionPart Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
            return this;
        }

        public VersionPart Default(object value)
        {
            structure.SetValue(Attr.Default, value.ToString());
            return this;
        }

        public VersionPart CustomType<T>()
        {
            structure.SetValue(Attr.Type, new TypeReference(typeof(T)));
            return this;
        }

        public VersionPart CustomType(Type type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public VersionPart CustomType(string type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public VersionPart CustomSqlType(string sqlType)
        {
            structure.SetValue(Attr.SqlType, sqlType);
            return this;
        }
    }
}