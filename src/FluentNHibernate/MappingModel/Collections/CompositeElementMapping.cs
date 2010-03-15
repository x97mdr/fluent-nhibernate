using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class CompositeElementMapping : MappingBase, ITypeMapping
    {
        readonly MappedMembers mappedMembers = new MappedMembers();
        readonly ValueStore values = new ValueStore();

        public CompositeElementMapping()
        {}

        public CompositeElementMapping(Type type)
        {
            Initialise(type);
        }

        public void Initialise(Type type)
        {
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCompositeElement(this);

            if (Parent != null)
                visitor.Visit(Parent);

            mappedMembers.AcceptVisitor(visitor);
        }

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public ParentMapping Parent { get; set; }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return mappedMembers.Properties; }
        }

        public void AddProperty(PropertyMapping property)
        {
            mappedMembers.AddProperty(property);
        }

        public IEnumerable<ManyToOneMapping> References
        {
            get { return mappedMembers.References; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddReference(ManyToOneMapping manyToOne)
        {
            mappedMembers.AddReference(manyToOne);
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(CompositeElementMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.mappedMembers, mappedMembers) && Equals(other.values, values) && Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CompositeElementMapping)) return false;
            return Equals((CompositeElementMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (mappedMembers != null ? mappedMembers.GetHashCode() : 0);
                result = (result * 397) ^ (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            mappedMembers.AddChild(child);

            if (child is ParentMapping)
                Parent = (ParentMapping)child;
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}