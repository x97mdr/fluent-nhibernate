using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ColumnPart
    {
        readonly IMappingStructure structure;
        bool nextBool = true;

        public ColumnPart(IMappingStructure structure)
        {
            this.structure = structure;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ColumnPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public ColumnPart Name(string columnName)
        {
            structure.SetValue(Attr.Name, columnName);
            return this;
        }

        public ColumnPart Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public ColumnPart Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public ColumnPart Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public ColumnPart UniqueKey(string key)
        {
            structure.SetValue(Attr.UniqueKey, key);
            return this;
        }

        public ColumnPart SqlType(string sqlType)
        {
            structure.SetValue(Attr.SqlType, sqlType);
            return this;
        }

        public ColumnPart Index(string index)
        {
            structure.SetValue(Attr.Index, index);
            return this;
        }
    }
}
