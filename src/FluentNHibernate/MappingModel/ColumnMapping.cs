using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ColumnMapping : MappingBase, IMapping
    {
        IEnumerable<KeyValuePair<Attr, object>> parentValues = new ValueStore();
        readonly ValueStore values = new ValueStore();

        public void SpecifyParentValues(IEnumerable<KeyValuePair<Attr, object>> newParentValues)
        {
            parentValues = newParentValues;
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessColumn(this);
        }

        public Member Member { get; set; }

        string GetEither(Attr attr)
        {
            return GetEither<string>(attr);
        }

        T GetEither<T>(Attr attr)
        {
            if (values.HasValue(attr))
                return values.Get<T>(attr);
            if (parentValues.Any(x => x.Key == attr))
                return parentValues
                    .Where(x => x.Key == attr)
                    .Select(x => x.Value)
                    .Cast<T>()
                    .Single();

            return default(T);
        }

        public string Name
        {
            get { return GetEither(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public int Length
        {
            get { return GetEither<int>(Attr.Length); }
            set { values.Set(Attr.Length, value); }
        }

        public bool NotNull
        {
            get { return GetEither<bool>(Attr.NotNull); }
            set { values.Set(Attr.NotNull, value); }
        }

        public bool Unique
        {
            get { return GetEither<bool>(Attr.Unique); }
            set { values.Set(Attr.Unique, value); }
        }

        public string UniqueKey
        {
            get { return GetEither(Attr.UniqueKey); }
            set { values.Set(Attr.UniqueKey, value); }
        }

        public string SqlType
        {
            get { return GetEither(Attr.SqlType); }
            set { values.Set(Attr.SqlType, value); }
        }

        public string Index
        {
            get { return GetEither(Attr.Index); }
            set { values.Set(Attr.Index, value); }
        }

        public string Check
        {
            get { return GetEither(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public int Precision
        {
            get { return GetEither<int>(Attr.Precision); }
            set { values.Set(Attr.Precision, value); }
        }

        public int Scale
        {
            get { return GetEither<int>(Attr.Scale); }
            set { values.Set(Attr.Scale, value); }
        }

        public string Default
        {
            get { return GetEither(Attr.Default); }
            set { values.Set(Attr.Default, value); }
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr) || parentValues.Any(x => x.Key == attr);
        }

        public bool Equals(ColumnMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) && Equals(other.Member, Member);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ColumnMapping)) return false;
            return Equals((ColumnMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^ (Member != null ? Member.GetHashCode() : 0);
            }
        }

        public void AddChild(IMapping child)
        {
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }
}