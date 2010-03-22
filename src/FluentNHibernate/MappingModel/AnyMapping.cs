using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class AnyMapping : MappingBase, IMemberMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IDefaultableList<ColumnMapping> typeColumns = new DefaultableList<ColumnMapping>();
        readonly IDefaultableList<ColumnMapping> identifierColumns = new DefaultableList<ColumnMapping>();
        readonly IList<MetaValueMapping> metaValues = new List<MetaValueMapping>();

        public void Initialise(Member member)
        {
            Name = member.Name;
            MetaType = new TypeReference(member.PropertyType);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessAny(this);

            foreach (var metaValue in metaValues)
                visitor.Visit(metaValue);

            foreach (var column in typeColumns)
                visitor.Visit(column);

            foreach (var column in identifierColumns)
                visitor.Visit(column);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string IdType
        {
            get { return values.Get(Attr.IdType); }
            set { values.Set(Attr.IdType, value); }
        }

        public TypeReference MetaType
        {
            get { return values.Get<TypeReference>(Attr.MetaType); }
            set { values.Set(Attr.MetaType, value); }
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

        public string Cascade
        {
            get { return values.Get(Attr.Cascade); }
            set { values.Set(Attr.Cascade, value); }
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

        public IDefaultableEnumerable<ColumnMapping> TypeColumns
        {
            get { return typeColumns; }
        }

        public IDefaultableEnumerable<ColumnMapping> IdentifierColumns
        {
            get { return identifierColumns; }
        }

        public IEnumerable<MetaValueMapping> MetaValues
        {
            get { return metaValues; }
        }

        public Type ContainingEntityType { get; set; }

        public void AddTypeDefaultColumn(ColumnMapping column)
        {
            typeColumns.AddDefault(column);
        }

        public void AddTypeColumn(ColumnMapping column)
        {
            typeColumns.Add(column);
        }

        public void AddIdentifierDefaultColumn(ColumnMapping column)
        {
            identifierColumns.AddDefault(column);
        }

        public void AddIdentifierColumn(ColumnMapping column)
        {
            identifierColumns.Add(column);
        }

        public void AddMetaValue(MetaValueMapping metaValue)
        {
            metaValues.Add(metaValue);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(AnyMapping other)
        {
            return Equals(other.values, values) &&
                other.typeColumns.ContentEquals(typeColumns) &&
                other.identifierColumns.ContentEquals(identifierColumns) &&
                other.metaValues.ContentEquals(metaValues) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AnyMapping)) return false;
            return Equals((AnyMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (typeColumns != null ? typeColumns.GetHashCode() : 0);
                result = (result * 397) ^ (identifierColumns != null ? identifierColumns.GetHashCode() : 0);
                result = (result * 397) ^ (metaValues != null ? metaValues.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            if (child is MetaValueMapping)
                AddMetaValue((MetaValueMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}