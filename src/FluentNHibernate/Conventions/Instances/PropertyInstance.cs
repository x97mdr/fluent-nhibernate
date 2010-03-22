using System;
using System.Diagnostics;
using System.Linq;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using NHibernate.Type;
using NHibernate.UserTypes;

namespace FluentNHibernate.Conventions.Instances
{
    public class PropertyInstance : PropertyInspector, IPropertyInstance
    {
        readonly ColumnInstanceHelper c;
        readonly PropertyMapping mapping;
        bool nextBool = true;

        public PropertyInstance(PropertyMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            c = new ColumnInstanceHelper(mapping);
        }

        public new void Insert()
        {
            if (!mapping.HasUserDefinedValue(Attr.Insert))
                mapping.Insert = nextBool;
            nextBool = true;
        }

        public new void Update()
        {
            if (!mapping.HasUserDefinedValue(Attr.Update))
                mapping.Update = nextBool;
            nextBool = true;
        }

        public new void ReadOnly()
        {
            if (!mapping.HasUserDefinedValue(Attr.Insert) && !mapping.HasUserDefinedValue(Attr.Update))
                mapping.Insert = mapping.Update = !nextBool;
            nextBool = true;
        }

        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Access))
                        mapping.Access = value;
                });
            }
        }

        public void CustomType(TypeReference type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Type))
            {
                mapping.Type = type;

                if (typeof(ICompositeUserType).IsAssignableFrom(mapping.Type.GetUnderlyingSystemType()))
                    AddColumnsForCompositeUserType();
            }
        }

        public void CustomType<T>()
        {
            CustomType(typeof(T));
        }

        public void CustomType(Type type)
        {
            CustomType(new TypeReference(type));
        }

        public void CustomType(string type)
        {
            CustomType(new TypeReference(type));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IPropertyInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public void Column(string columnName)
        {
            if (mapping.Columns.UserDefined.Count() > 0)
                return;

            var originalColumn = mapping.Columns.FirstOrDefault();
            var column = originalColumn == null ? new ColumnMapping() : originalColumn; // TODO: fix

            column.Name = columnName;

            mapping.ClearColumns();
            mapping.AddColumn(column);
        }

        public new void Formula(string formula)
        {
            if (!mapping.HasUserDefinedValue(Attr.Formula))
                mapping.Formula = formula;
        }

        public new IGeneratedInstance Generated
        {
            get
            {
                return new GeneratedInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Generated))
                        mapping.Generated = value;
                });
            }
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void OptimisticLock()
        {
            if (!mapping.HasUserDefinedValue(Attr.OptimisticLock))
                mapping.OptimisticLock = nextBool;
            nextBool = true;
        }

        public new void Nullable()
        {
            if (!c.ThisOrColumnHasUserDefinedValue(Attr.NotNull))
                c.SetOnEachColumn(x => x.NotNull = !nextBool);

            nextBool = true;
        }

        public void CustomSqlType(string sqlType)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.SqlType))
                return;
         
            c.SetOnEachColumn(x => x.SqlType = sqlType);
        }

        public new void Precision(int precision)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Precision))
                return;

            c.SetOnEachColumn(x => x.Precision = precision);
        }

        public new void Scale(int scale)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Scale))
                return;

            c.SetOnEachColumn(x => x.Scale = scale);
        }

        public new void Default(string value)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Default))
                return;

            c.SetOnEachColumn(x => x.Default = value);
        }

        public new void Unique()
        {
            if (!c.ThisOrColumnHasUserDefinedValue(Attr.Unique))
                c.SetOnEachColumn(x => x.Unique = nextBool);

            nextBool = true;
        }

        public new void UniqueKey(string keyName)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.UniqueKey))
                return;

            c.SetOnEachColumn(x => x.UniqueKey = keyName);
        }

        public new void Length(int length)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Length))
                return;

            c.SetOnEachColumn(x => x.Length = length);
        }

        public new void Index(string value)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Index))
                return;

            c.SetOnEachColumn(x => x.Index = value);
        }

        public new void Check(string constraint)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Check))
                return;

            c.SetOnEachColumn(x => x.Check = constraint);
        }

        private void AddColumnsForCompositeUserType()
        {
            var inst = (ICompositeUserType)Activator.CreateInstance(mapping.Type.GetUnderlyingSystemType());

            if (inst.PropertyNames.Length > 1)
            {
                var existingColumn = mapping.Columns.Single();
                mapping.ClearColumns();
                var propertyPrefix = existingColumn.Name;
                for (int i = 0; i < inst.PropertyNames.Length; i++)
                {
                    var propertyName = inst.PropertyNames[i];
                    var propertyType = inst.PropertyTypes[i];

                    var column = existingColumn; // TODO: Fix - clone
                    column.Name = propertyPrefix + "_" + propertyName;
                    mapping.AddColumn(column);
                }
            }
        }
    }
}