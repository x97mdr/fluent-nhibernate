using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ComponentMapping : ComponentMappingBase, IComponentMapping, ITypeMapping, IMemberMapping
    {
        readonly ValueStore values;
        public ComponentType ComponentType { get; set; }

        public ComponentMapping()
            : this(new ValueStore())
        {}

        public ComponentMapping(ComponentType componentType)
            : this(new ValueStore())
        {
            ComponentType = componentType;
        }

        ComponentMapping(ValueStore values)
            : base(values)
        {
            this.values = values;
        }

        public void Initialise(Type type)
        {
            Class = new TypeReference(type);
        }

        public void Initialise(Member member)
        {
            Name = member.Name;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessComponent(this);

            base.AcceptVisitor(visitor);
        }

        public override void MergeAttributes(AttributeStore store)
        {
        }

        public override string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public override Type Type
        {
            get { return values.Get<Type>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public override bool HasValue(Attr property)
        {
            return values.HasValue(property);
        }

        public bool Equals(ComponentMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ComponentMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (values != null ? values.GetHashCode() : 0);
                }
            }
        }

        public override void AddChild(IMapping child)
        {
            base.AddChild(child);


        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}