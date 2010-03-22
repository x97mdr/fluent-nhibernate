using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class ManyToManyMapping : MappingBase, ICollectionRelationshipMapping, IHasColumnMappings, IMapping, ITypeMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();

        public void Initialise(Type type)
        {
            Class = new TypeReference(type);

            var column = new ColumnMapping { Name = type.Name + "_id" };
            column.SpecifyParentValues(values);
            AddDefaultColumn(column);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessManyToMany(this);

            foreach (var column in columns)
                visitor.Visit(column);
        }

        public Type ChildType
        {
            get { return values.Get<Type>(Attr.ChildType); }
            set { values.Set(Attr.ChildType, value); }
        }

        public Type ParentType
        {
            get { return values.Get<Type>(Attr.ParentType); }
            set { values.Set(Attr.ParentType, value); }
        }

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public string ForeignKey
        {
            get { return values.Get(Attr.ForeignKey); }
            set { values.Set(Attr.ForeignKey, value); }
        }

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public string NotFound
        {
            get { return values.Get(Attr.NotFound); }
            set { values.Set(Attr.NotFound, value); }
        }

        public string Where
        {
            get { return values.Get(Attr.Where); }
            set { values.Set(Attr.Where, value); }
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

        public IDefaultableEnumerable<ColumnMapping> Columns
        {
            get { return columns; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddColumn(ColumnMapping column)
        {
            columns.Add(column);
        }

        public void AddDefaultColumn(ColumnMapping column)
        {
            columns.AddDefault(column);
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

        public bool Equals(ManyToManyMapping other)
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
            if (obj.GetType() != typeof(ManyToManyMapping)) return false;
            return Equals((ManyToManyMapping)obj);
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
