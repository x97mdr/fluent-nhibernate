using System;
using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Conventions.Instances
{
    public class ClassInstance : ClassInspector, IClassInstance
    {
        private readonly ClassMapping mapping;
        private bool nextBool = true;

        public ClassInstance(ClassMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IClassInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new ISchemaActionInstance SchemaAction
        {
            get
            {
                return new SchemaActionInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.SchemaAction))
                        mapping.SchemaAction = value;
                });
            }
        }

        public void Table(string tableName)
        {
            if (!mapping.HasUserDefinedValue(Attr.Table))
                mapping.TableName = tableName;
        }

        public new void DynamicInsert()
        {
            if (!mapping.HasUserDefinedValue(Attr.DynamicInsert))
                mapping.DynamicInsert = nextBool;
            nextBool = true;
        }

        public new void DynamicUpdate()
        {
            if (!mapping.HasUserDefinedValue(Attr.DynamicUpdate))
                mapping.DynamicUpdate = nextBool;
            nextBool = true;
        }

        public new IOptimisticLockInstance OptimisticLock
        {
            get
            {
                return new OptimisticLockInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.OptimisticLock))
                        mapping.OptimisticLock = value;
                });
            }
        }

        public new void BatchSize(int size)
        {
            if (!mapping.HasUserDefinedValue(Attr.BatchSize))
                mapping.BatchSize = size;
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }


        public new void ReadOnly()
        {
            if (!mapping.HasUserDefinedValue(Attr.Mutable))
                mapping.Mutable = !nextBool;
            nextBool = true;
        }

        public new void Schema(string schema)
        {
            if (!mapping.HasUserDefinedValue(Attr.Schema))
                mapping.Schema = schema;
        }

        public new void Where(string where)
        {
            if (!mapping.HasUserDefinedValue(Attr.Where))
                mapping.Where = where;
        }

        public new void Subselect(string subselectSql)
        {
            if (!mapping.HasUserDefinedValue(Attr.Subselect))
                mapping.Subselect = subselectSql;
        }

        public new void Proxy<T>()
        {
            Proxy(typeof(T));
        }

        public new void Proxy(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = type.AssemblyQualifiedName;
        }

        public new void Proxy(string type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = type;
        }
    }
}