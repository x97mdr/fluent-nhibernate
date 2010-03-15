using System;
using System.Linq;

namespace FluentNHibernate.MappingModel
{
    public abstract class ColumnBasedMappingBase : MappingBase, IHasColumnMappings
    {
        private readonly string[] columnAttributes = new[] { "Length", "Precision", "Scale", "NotNull", "Unique", "UniqueKey", "SqlType", "Index", "Check", "Default" };
        protected readonly IDefaultableList<ColumnMapping> columns = new DefaultableList<ColumnMapping>();

        public virtual void AddChild(IMapping child)
        {
            if (child is ColumnMapping)
                columns.Add((ColumnMapping)child);
        }

        public override bool IsSpecified(string property)
        {
            if (columnAttributes.Contains(property))
                return columns.Any(x => x.IsSpecified(property));

            return false;
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

        public bool Equals(ColumnBasedMappingBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.columns.ContentEquals(columns);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ColumnBasedMappingBase)) return false;
            return Equals((ColumnBasedMappingBase)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((columns != null ? columns.GetHashCode() : 0) * 397);
            }
        }
    }
}