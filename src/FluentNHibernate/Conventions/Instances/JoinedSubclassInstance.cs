using System;
using System.Diagnostics;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Conventions.Instances
{
    public class JoinedSubclassInstance : JoinedSubclassInspector, IJoinedSubclassInstance
    {
        private readonly SubclassMapping mapping;
        private bool nextBool = true;

        public JoinedSubclassInstance(SubclassMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new IKeyInstance Key
        {
            get
            {
                if (mapping.Key == null)
                    mapping.Key = new KeyMapping();

                return new KeyInstance(mapping.Key);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IJoinedSubclassInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new void Abstract()
        {
            if (!mapping.HasUserDefinedValue(Attr.Abstract))
                mapping.Abstract = nextBool;
            nextBool = true;
        }

        public new void Check(string constraint)
        {
            if (!mapping.HasUserDefinedValue(Attr.Check))
                mapping.Check = constraint;
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

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void Proxy(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = type.AssemblyQualifiedName;
        }

        public new void Proxy<T>()
        {
            if (!mapping.HasUserDefinedValue(Attr.Proxy))
                mapping.Proxy = typeof(T).AssemblyQualifiedName;
        }

        public void Schema(string schema)
        {
            if (!mapping.HasUserDefinedValue(Attr.Schema))
                mapping.Schema = schema;
        }

        public new void SelectBeforeUpdate()
        {
            if (!mapping.HasUserDefinedValue(Attr.SelectBeforeUpdate))
                mapping.SelectBeforeUpdate = nextBool;
            nextBool = true;
        }

        public void Table(string tableName)
        {
            if (!mapping.HasUserDefinedValue(Attr.Table))
                mapping.TableName = tableName;
        }

        public void Subselect(string subselect)
        {
            if (!mapping.HasUserDefinedValue(Attr.Subselect))
                mapping.Subselect = subselect;
        }

        public void Persister<T>() where T : IEntityPersister
        {
            if (!mapping.HasUserDefinedValue(Attr.Persister))
                mapping.Persister = new TypeReference(typeof(T));
        }

        public void Persister(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Persister))
                mapping.Persister = new TypeReference(type);
        }

        public void Persister(string type)
        {
            if (!mapping.HasUserDefinedValue(Attr.Persister))
                mapping.Persister = new TypeReference(type);
        }

        public void BatchSize(int batchSize)
        {
            if (!mapping.HasUserDefinedValue(Attr.BatchSize))
                mapping.BatchSize = batchSize;
        }
    }
}