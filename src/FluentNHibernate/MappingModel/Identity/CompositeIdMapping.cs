using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class CompositeIdMapping : MappingBase, IIdentityMapping, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IList<KeyPropertyMapping> keyProperties = new List<KeyPropertyMapping>();
        readonly IList<KeyManyToOneMapping> keyManyToOnes = new List<KeyManyToOneMapping>();

        public CompositeIdMapping()
        {
            values.SetDefault(Attr.Mapped, false);
            values.SetDefault(Attr.UnsavedValue, "undefined");
        }

        public void Initialise(Member member)
        {
            Name = member.Name;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCompositeId(this);

            foreach (var key in keyProperties)
                visitor.Visit(key);

            foreach (var key in keyManyToOnes)
                visitor.Visit(key);
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

        public bool Mapped
        {
            get { return values.Get<bool>(Attr.Mapped); }
            set { values.Set(Attr.Mapped, value); }
        }

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public string UnsavedValue
        {
            get { return values.Get(Attr.UnsavedValue); }
            set { values.Set(Attr.UnsavedValue, value); }
        }

        public IEnumerable<KeyPropertyMapping> KeyProperties
        {
            get { return keyProperties; }
        }

        public IEnumerable<KeyManyToOneMapping> KeyManyToOnes
        {
            get { return keyManyToOnes; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddKeyProperty(KeyPropertyMapping mapping)
        {
            keyProperties.Add(mapping);
        }

        public void AddKeyManyToOne(KeyManyToOneMapping mapping)
        {
            keyManyToOnes.Add(mapping);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasUserDefinedValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(CompositeIdMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.keyProperties.ContentEquals(keyProperties) &&
                other.keyManyToOnes.ContentEquals(keyManyToOnes) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CompositeIdMapping)) return false;
            return Equals((CompositeIdMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (keyProperties != null ? keyProperties.GetHashCode() : 0);
                result = (result * 397) ^ (keyManyToOnes != null ? keyManyToOnes.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            if (child is KeyPropertyMapping)
                AddKeyProperty((KeyPropertyMapping)child);
            if (child is KeyManyToOneMapping)
                AddKeyManyToOne((KeyManyToOneMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}