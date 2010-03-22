using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class KeyMapping : MappingBase, IHasColumnMappings, ITypeMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();
        public Type ContainingEntityType { get; set; }

        public void Initialise(Type type)
        {
            var column = new ColumnMapping { Name = type.Name + "_id" };
            column.SpecifyParentValues(values);
            AddDefaultColumn(column);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessKey(this);

            foreach (var column in columns)
                visitor.Visit(column);
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

        public string OnDelete
        {
            get { return values.Get(Attr.OnDelete); }
            set { values.Set(Attr.OnDelete, value); }
        }

        public bool NotNull
        {
            get { return values.Get<bool>(Attr.NotNull); }
            set { values.Set(Attr.NotNull, value); }
        }

        public bool Update
        {
            get { return values.Get<bool>(Attr.Update); }
            set { values.Set(Attr.Update, value); }
        }

        public bool Unique
        {
            get { return values.Get<bool>(Attr.Unique); }
            set { values.Set(Attr.Unique, value); }
        }

        public IDefaultableEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public void AddColumn(ColumnMapping mapping)
        {
            columns.Add(mapping);
        }

        public void AddDefaultColumn(ColumnMapping mapping)
        {
            columns.AddDefault(mapping);
        }

        public void ClearColumns()
        {
            columns.Clear();
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(KeyMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.columns.ContentEquals(columns) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(KeyMapping)) return false;
            return Equals((KeyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (columns != null ? columns.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            if (child is ColumnMapping)
                AddColumn((ColumnMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}