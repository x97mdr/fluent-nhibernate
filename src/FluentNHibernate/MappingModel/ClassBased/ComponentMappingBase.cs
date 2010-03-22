using System;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public abstract class ComponentMappingBase : ClassMappingBase
    {
        readonly ValueStore values;

        protected ComponentMappingBase(ValueStore values)
        {
            this.values = values;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            if (Parent != null)
                visitor.Visit(Parent);

            base.AcceptVisitor(visitor);
        }

        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }
        public ParentMapping Parent { get; set; }

        public bool Unique
        {
            get { return values.Get<bool>(Attr.Unique); }
            set { values.Set(Attr.Unique, value); }
        }

        public bool Insert
        {
            get { return values.Get<bool>(Attr.Insert); }
            set { values.Set(Attr.Insert, value); }
        }

        public bool Update
        {
            get { return values.Get<bool>(Attr.Update); }
            set { values.Set(Attr.Update, value); }
        }

        public string Access
        {
            get { return values.Get(Attr.Access); }
            set { values.Set(Attr.Access, value); }
        }

        public bool OptimisticLock
        {
            get { return values.Get<bool>(Attr.OptimisticLock); }
            set { values.Set(Attr.OptimisticLock, value); }
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            if (property == Attr.Parent)
                return Parent != null;

            return HasValue(property);
        }

        public abstract bool HasValue(Attr attr);

        public override void AddChild(IMapping child)
        {
            base.AddChild(child);

            if (child is ParentMapping)
                Parent = (ParentMapping)child;
        }

        public bool Equals(ComponentMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.values, values) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ComponentMappingBase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                result = (result * 397) ^ (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }
    }
}