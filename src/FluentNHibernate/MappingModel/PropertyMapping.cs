using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class PropertyMapping : ColumnBasedMappingBase, IMapping, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();

        public void Initialise(Member member)
        {
            Name = member.Name;
            Type = GetType(member);

            // TODO: need a more explicit way to show that we're setting
            // a property for the columns here
            if (member.PropertyType.Closes(typeof(Nullable<>)))
                values.Set(Attr.NotNull, false);

            var column = new ColumnMapping { Name = member.Name };
            column.SpecifyParentValues(values);
            AddDefaultColumn(column);
        }

        static TypeReference GetType(Member member)
        {
            var type = member.PropertyType;

            if (type.Closes(typeof(Nullable<>)))
                type = type.GetGenericArguments()[0];

            if (type.IsEnum)
                type = typeof(GenericEnumMapper<>).MakeGenericType(type);

            return new TypeReference(type);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessProperty(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ContainingEntityType { get; set; }

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

        public string Formula
        {
            get { return values.Get(Attr.Formula); }
            set { values.Set(Attr.Formula, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public bool OptimisticLock
        {
            get { return values.Get<bool>(Attr.OptimisticLock); }
            set { values.Set(Attr.OptimisticLock, value); }
        }

        public string Generated
        {
            get { return values.Get(Attr.Generated); }
            set { values.Set(Attr.Generated, value); }
        }

        public TypeReference Type
        {
            get { return values.Get<TypeReference>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public Member Member { get; set; }

        public bool Equals(PropertyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.ContainingEntityType, ContainingEntityType) &&
                Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(PropertyMapping)) return false;
            return Equals((PropertyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0) * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }
    }
}