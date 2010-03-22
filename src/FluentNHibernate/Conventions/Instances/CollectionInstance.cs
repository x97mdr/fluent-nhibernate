using System;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Conventions.Instances
{
    public class CollectionInstance : CollectionInspector, ICollectionInstance
    {
        private readonly CollectionMapping mapping;
        protected bool nextBool = true;

        public CollectionInstance(CollectionMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new IRelationshipInstance Relationship
        {
            get
            {
                if (mapping.Relationship is ManyToManyMapping)
                    return new ManyToManyInstance((ManyToManyMapping)mapping.Relationship);

                return new OneToManyInstance((OneToManyMapping)mapping.Relationship);
            }
        }

        public new ICollectionCascadeInstance Cascade
        {
            get
            {
                return new CollectionCascadeInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Cascade))
                        mapping.Cascade = value;
                });
            }
        }

        public new IFetchInstance Fetch
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

        public new void Check(string constraint)
        {
            if (!mapping.HasUserDefinedValue(Attr.Check))
                mapping.Check = constraint;
        }

        public new void CollectionType<T>()
        {
            if (!mapping.HasUserDefinedValue(Attr.CollectionType))
                mapping.CollectionType = new TypeReference(typeof(T));
        }

        public new void CollectionType(string type)
        {
            if (!mapping.HasUserDefinedValue(Attr.CollectionType))
                mapping.CollectionType = new TypeReference(type);
        }

        public new void CollectionType(Type type)
        {
            if (!mapping.HasUserDefinedValue(Attr.CollectionType))
                mapping.CollectionType = new TypeReference(type);
        }

        public new void Generic()
        {
            if (!mapping.HasUserDefinedValue(Attr.Generic))
                mapping.Generic = nextBool;
            nextBool = true;
        }

        public new void Inverse()
        {
            if (!mapping.HasUserDefinedValue(Attr.Inverse))
                mapping.Inverse = nextBool;
            nextBool = true;
        }

        public new void Persister<T>() where T : IEntityPersister
        {
            if (!mapping.HasUserDefinedValue(Attr.Persister))
                mapping.Persister = new TypeReference(typeof(T));
        }

        public new void Where(string whereClause)
        {
            if (!mapping.HasUserDefinedValue(Attr.Where))
                mapping.Where = whereClause;
        }

        public new void OrderBy(string orderBy)
        {
            if (!mapping.HasUserDefinedValue(Attr.OrderBy))
                mapping.OrderBy = orderBy;
        }

        public void Subselect(string subselect)
        {
            if (!mapping.HasUserDefinedValue(Attr.Subselect))
                mapping.Subselect = subselect;
        }

        public void Table(string tableName)
        {
            if (!mapping.HasUserDefinedValue(Attr.Table))
                mapping.TableName = tableName;
        }

        public new void Name(string name)
        {
            if (!mapping.HasUserDefinedValue(Attr.Name))
                mapping.Name = name;
        }

        public new void Schema(string schema)
        {
            if (!mapping.HasUserDefinedValue(Attr.Schema))
                mapping.Schema = schema;
        }

        public new void LazyLoad()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new void BatchSize(int batchSize)
        {
            if (!mapping.HasUserDefinedValue(Attr.BatchSize))
                mapping.BatchSize = batchSize;
        }

        public void ReadOnly()
        {
            if (!mapping.HasUserDefinedValue(Attr.Mutable))
                mapping.Mutable = !nextBool;
            nextBool = true;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ICollectionInstance Not
        {
            get 
            {
                nextBool = !nextBool;
                return this; 
            }
        }
        
        public new ICacheInstance Cache
        {
            get
            {
                if (mapping.Cache == null)
                    // conventions are hitting it, user must want a cache
                    mapping.Cache = new CacheMapping();

                return new CacheInstance(mapping.Cache);
            }
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

        public new IKeyInstance Key
        {
            get { return new KeyInstance(mapping.Key); }
        }
    }
}