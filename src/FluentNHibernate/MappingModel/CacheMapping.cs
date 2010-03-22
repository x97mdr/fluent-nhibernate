using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class CacheMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCache(this);
        }

        public string Region
        {
            get { return values.Get(Attr.Region); }
            set { values.Set(Attr.Region, value); }
        }

        public string Usage
        {
            get { return values.Get(Attr.Usage); }
            set { values.Set(Attr.Usage, value); }
        }

        public string Include
        {
            get { return values.Get(Attr.Include); }
            set { values.Set(Attr.Include, value); }
        }

        public Type ContainedEntityType { get; set; }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public bool Equals(CacheMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) && Equals(other.ContainedEntityType, ContainedEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CacheMapping)) return false;
            return Equals((CacheMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^ (ContainedEntityType != null ? ContainedEntityType.GetHashCode() : 0);
            }
        }

        public void AddChild(IMapping child)
        {
            throw new NotImplementedException();
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