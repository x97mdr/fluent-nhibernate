using System;
using System.Diagnostics;
using System.Linq;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Instances
{
    public class ManyToOneInstance : ManyToOneInspector, IManyToOneInstance
    {
        readonly ColumnInstanceHelper c;
        readonly ManyToOneMapping mapping;
        bool nextBool = true;

        public ManyToOneInstance(ManyToOneMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
            c = new ColumnInstanceHelper(mapping);
        }

        public void Column(string columnName)
        {
            if (mapping.Columns.UserDefined.Count() > 0)
                return;

            var originalColumn = mapping.Columns.FirstOrDefault();
            var column = originalColumn == null ? new ColumnMapping() : originalColumn; // TODO: Fix

            column.Name = columnName;

            mapping.ClearColumns();
            mapping.AddColumn(column);
        }

        public void CustomClass<T>()
        {
            if (!mapping.HasUserDefinedValue(Attr.Class))
                mapping.Class = new TypeReference(typeof(T));
        }

        public void CustomClass(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Class))
                mapping.Class = new TypeReference(type);
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

        public new ICascadeInstance Cascade
        {
            get
            {
                return new CascadeInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Cascade))
                        mapping.Cascade = value;
                });
            }
        }

        new public IFetchInstance Fetch
        {
            get
            {
                return new FetchInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Fetch))
                        mapping.Fetch = value;
                });
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IManyToOneInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new INotFoundInstance NotFound
        {
            get
            {
                return new NotFoundInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.NotFound))
                        mapping.NotFound = value;
                });
            }
        }

        public new void Insert()
        {
            if (!mapping.HasUserDefinedValue(Attr.Insert))
                mapping.Insert = nextBool;
            nextBool = true;
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void PropertyRef(string property)
        {
            if (!mapping.HasUserDefinedValue(Attr.PropertyRef))
                mapping.PropertyRef = property;
        }

        public void ReadOnly()
        {
            if (!mapping.HasUserDefinedValue(Attr.Insert) && !mapping.HasUserDefinedValue(Attr.Update))
            {
                mapping.Insert = !nextBool;
                mapping.Update = !nextBool;
            }
            nextBool = true;
        }

        public void Index(string index)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.Index))
                return;

            c.SetOnEachColumn(x => x.Index = index);
        }

        public void Nullable()
        {
            if (!c.ThisOrColumnHasUserDefinedValue(Attr.NotNull))
                c.SetOnEachColumn(x => x.NotNull = !nextBool);

            nextBool = true;
        }

        public void Unique()
        {
            if (!c.ThisOrColumnHasUserDefinedValue(Attr.Unique))
                c.SetOnEachColumn(x => x.Unique = nextBool);

            nextBool = true;
        }

        public void UniqueKey(string key)
        {
            if (c.ThisOrColumnHasUserDefinedValue(Attr.UniqueKey))
                return;

            c.SetOnEachColumn(x => x.UniqueKey = key);
        }

        public new void Update()
        {
            if (!mapping.HasUserDefinedValue(Attr.Update))
                mapping.Update = nextBool;
            nextBool = true;
        }

        public new void ForeignKey(string key)
        {
            if (!mapping.HasUserDefinedValue(Attr.ForeignKey))
                mapping.ForeignKey = key;
        }

        public void OverrideInferredClass(Type type)
        {
            mapping.Class = new TypeReference(type);
        }
    }
}