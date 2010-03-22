using System;
using System.Collections.Generic;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class VersionMapping : ColumnBasedMappingBase, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Member member)
        {
            Name = member.Name;
            values.SetDefault(Attr.Type, member.PropertyType == typeof(DateTime) ? new TypeReference("timestamp") : new TypeReference(member.PropertyType));
            
            var column = new ColumnMapping { Name = member.Name };
            column.SpecifyParentValues(values);
            AddDefaultColumn(column);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            if (base.HasUserDefinedValue(property))
                return true;

            return values.HasUserDefinedValue(property);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessVersion(this);

            columns.Each(visitor.Visit);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return values.Get(Attr.Access); }
            set { values.Set(Attr.Access, value); }
        }

        public TypeReference Type
        {
            get { return values.Get<TypeReference>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public string UnsavedValue
        {
            get { return values.Get(Attr.UnsavedValue); }
            set { values.Set(Attr.UnsavedValue, value); }
        }

        public string Generated
        {
            get { return values.Get(Attr.Generated); }
            set { values.Set(Attr.Generated, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(VersionMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as VersionMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                }
            }
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }
    }
}