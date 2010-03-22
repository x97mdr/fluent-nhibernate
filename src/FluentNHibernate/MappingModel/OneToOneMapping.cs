using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class OneToOneMapping : MappingBase, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Member member)
        {
            Name = member.Name;
            values.SetDefault(Attr.Class, member.PropertyType);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessOneToOne(this);
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

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public string Cascade
        {
            get { return values.Get(Attr.Cascade); }
            set { values.Set(Attr.Cascade, value); }
        }

        public bool Constrained
        {
            get { return values.Get<bool>(Attr.Constrained); }
            set { values.Set(Attr.Constrained, value); }
        }

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public string ForeignKey
        {
            get { return values.Get(Attr.ForeignKey); }
            set { values.Set(Attr.ForeignKey, value); }
        }

        public string PropertyRef
        {
            get { return values.Get(Attr.PropertyRef); }
            set { values.Set(Attr.PropertyRef, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public string EntityName
        {
            get { return values.Get(Attr.EntityName); }
            set { values.Set(Attr.EntityName, value); }
        }

        public Type ContainingEntityType { get; set; }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasUserDefinedValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(OneToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(OneToOneMapping)) return false;
            return Equals((OneToOneMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
            }
        }

        public void AddChild(IMapping child)
        {
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}