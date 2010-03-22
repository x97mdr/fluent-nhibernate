using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParentMapping : MappingBase, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Member member)
        {
            Name = member.Name;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessParent(this);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public Type ContainingEntityType { get; set; }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ParentMapping)) return false;
            
            return Equals((ParentMapping)obj);
        }

        public bool Equals(ParentMapping other)
        {
            return Equals(other.values, values) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^
                    (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
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