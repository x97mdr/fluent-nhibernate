using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class JoinMapping : IMappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly MappedMembers mappedMembers = new MappedMembers();

        public KeyMapping Key { get; set; }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return mappedMembers.Properties; }
        }

        public IEnumerable<ManyToOneMapping> References
        {
            get { return mappedMembers.References; }
        }

        public IEnumerable<IComponentMapping> Components
        {
            get { return mappedMembers.Components; }
        }

        public IEnumerable<AnyMapping> Anys
        {
            get { return mappedMembers.Anys; }
        }

        public void AddProperty(PropertyMapping property)
        {
            mappedMembers.AddProperty(property);
        }

        public void AddReference(ManyToOneMapping manyToOne)
        {
            mappedMembers.AddReference(manyToOne);
        }

        public void AddComponent(IComponentMapping componentMapping)
        {
            mappedMembers.AddComponent(componentMapping);
        }

        public void AddAny(AnyMapping mapping)
        {
            mappedMembers.AddAny(mapping);
        }

        public string TableName
        {
            get { return values.Get(Attr.Table); }
            set { values.Set(Attr.Table, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
        }

        public string Catalog
        {
            get { return values.Get(Attr.Catalog); }
            set { values.Set(Attr.Catalog, value); }
        }

        public string Subselect
        {
            get { return values.Get(Attr.Subselect); }
            set { values.Set(Attr.Subselect, value); }
        }

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public bool Inverse
        {
            get { return values.Get<bool>(Attr.Inverse); }
            set { values.Set(Attr.Inverse, value); }
        }

        public bool Optional
        {
            get { return values.Get<bool>(Attr.Optional); }
            set { values.Set(Attr.Optional, value); }
        }

        public Type ContainingEntityType { get; set; }

        public void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessJoin(this);

            if (Key != null)
                visitor.Visit(Key);

            mappedMembers.AcceptVisitor(visitor);
        }

        public bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(JoinMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                Equals(other.mappedMembers, mappedMembers) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(JoinMapping)) return false;
            return Equals((JoinMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (mappedMembers != null ? mappedMembers.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            mappedMembers.AddChild(child);

            if (child is KeyMapping)
                Key = (KeyMapping)child;
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}
