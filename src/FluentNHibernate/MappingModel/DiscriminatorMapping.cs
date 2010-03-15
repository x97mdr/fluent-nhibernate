using System;
using System.Collections.Generic;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class DiscriminatorMapping : ColumnBasedMappingBase, ITypeMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Type type)
        {
            Type = new TypeReference(type);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessDiscriminator(this);

            columns.Each(visitor.Visit);
        }

        public bool Force
        {
            get { return values.Get<bool>(Attr.Force); }
            set { values.Set(Attr.Force, value); }
        }

        public bool Insert
        {
            get { return values.Get<bool>(Attr.Insert); }
            set { values.Set(Attr.Insert, value); }
        }

        public string Formula
        {
            get { return values.Get(Attr.Formula); }
            set { values.Set(Attr.Formula, value); }
        }

        public TypeReference Type
        {
            get { return values.Get<TypeReference>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public Type ContainingEntityType { get; set; }

        public bool Equals(DiscriminatorMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.ContainingEntityType, ContainingEntityType) &&
                other.columns.ContentEquals(columns) &&
                Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DiscriminatorMapping)) return false;
            return Equals((DiscriminatorMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0) * 397) ^ ((columns != null ? columns.GetHashCode() : 0) * 397) ^ (values != null ? values.GetHashCode() : 0);
            }
        }

        public override void AddChild(IMapping child)
        {
            if (child is ColumnMapping)
                AddColumn((ColumnMapping)child);
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
        
        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }
    }
}
