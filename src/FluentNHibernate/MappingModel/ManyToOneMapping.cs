using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ManyToOneMapping : ColumnBasedMappingBase, IHasColumnMappings, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Member member)
        {
            Name = member.Name;
            Member = member;
            values.SetDefault(Attr.Class, new TypeReference(member.PropertyType));

            var column = new ColumnMapping { Name = member.Name + "_id" };
            column.SpecifyParentValues(values);
            AddDefaultColumn(column);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessManyToOne(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }

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

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public bool Update
        {
            get { return values.Get<bool>(Attr.Update); }
            set { values.Set(Attr.Update, value); }
        }

        public bool Insert
        {
            get { return values.Get<bool>(Attr.Insert); }
            set { values.Set(Attr.Insert, value); }
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

        public string NotFound
        {
            get { return values.Get(Attr.NotFound); }
            set { values.Set(Attr.NotFound, value); }
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

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasUserDefinedValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(ManyToOneMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.columns.ContentEquals(columns) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ManyToOneMapping)) return false;
            return Equals((ManyToOneMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                result = (result * 397) ^ (Member != null ? Member.GetHashCode() : 0);
                return result;
            }
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}