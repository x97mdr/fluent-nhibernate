using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class FilterMapping : IMappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Condition
        {
            get { return values.Get(Attr.Condition); }
            set { values.Set(Attr.Condition, value); }
        }

        public void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessFilter(this);
        }

        public bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public bool Equals(FilterMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FilterMapping)) return false;
            return Equals((FilterMapping)obj);
        }

        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
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
