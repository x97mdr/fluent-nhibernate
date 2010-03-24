using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParamMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public void AddChild(IMapping child)
        {
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Value
        {
            get { return values.Get(Attr.Value); }
            set { values.Set(Attr.Value, value); }
        }

        public bool Equals(ParamMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ParamMapping)) return false;
            return Equals((ParamMapping)obj);
        }

        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
        }
    }
}