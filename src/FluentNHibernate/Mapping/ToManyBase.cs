using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public abstract class ToManyBase<T, TChild, TRelationshipAttributes>
        where T : ToManyBase<T, TChild, TRelationshipAttributes>
        where TRelationshipAttributes : ICollectionRelationshipMapping
    {
        readonly IMappingStructure<CollectionMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        readonly IMappingStructure relationshipStructure;
        IMappingStructure<CacheMapping> cacheStructure;
        readonly AccessStrategyBuilder<T> access;
        readonly FetchTypeExpression<T> fetch;
        readonly OptimisticLockBuilder<T> optimisticLock;
        readonly CollectionCascadeExpression<T> cascade;
        protected bool nextBool = true;

        protected ToManyBase(IMappingStructure<CollectionMapping> structure, IMappingStructure<KeyMapping> keyStructure, IMappingStructure relationshipStructure)
        {
            this.structure = structure;
            this.keyStructure = keyStructure;
            this.relationshipStructure = relationshipStructure;
            
            structure.AddChild(keyStructure);
            structure.AddChild(relationshipStructure);

            access = new AccessStrategyBuilder<T>((T)this, value => structure.SetValue(Attr.Access, value));
            fetch = new FetchTypeExpression<T>((T)this, value => structure.SetValue(Attr.Fetch, value));
            optimisticLock = new OptimisticLockBuilder<T>((T)this, value => structure.SetValue(Attr.OptimisticLock, value));
            cascade = new CollectionCascadeExpression<T>((T)this, value => structure.SetValue(Attr.Cascade, value));
        }

        /// <summary>
        /// This method is used to set a different key column in this table to be used for joins.
        /// The output is set as the property-ref attribute in the "key" subelement of the collection
        /// </summary>
        /// <param name="propertyRef">The name of the column in this table which is linked to the foreign key</param>
        /// <returns>OneToManyPart</returns>
        public T PropertyRef(string propertyRef)
        {
            keyStructure.SetValue(Attr.PropertyRef, propertyRef);
            return (T)this;
        }

        /// <summary>
        /// Specify caching for this entity.
        /// </summary>
        public CachePart Cache
        {
            get
            {
                if (cacheStructure == null)
                {
                    cacheStructure = new FreeStructure<CacheMapping>();
                    structure.AddChild(cacheStructure);
                }

                return new CachePart(cacheStructure);
            }
        }

        public T LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return (T)this;
        }

        public T Inverse()
        {
            structure.SetValue(Attr.Inverse, nextBool);
            nextBool = true;
            return (T)this;
        }

        public CollectionCascadeExpression<T> Cascade
        {
            get { return cascade; }
        }

        public T AsSet()
        {
            structure.Alter(x => x.Type = Collection.Set);
            return (T)this;
        }

        void SetSort(SortType sort)
        {
            structure.SetValue(Attr.Sort, sort.ToString());
        }

        void SetSort(string sort)
        {
            structure.SetValue(Attr.Sort, sort);
        }

        public T AsSet(SortType sort)
        {
            SetSort(sort);
            structure.Alter(x =>
            {
                x.Type = Collection.Set;
            });
            return (T)this;
        }

        public T AsSet<TComparer>() where TComparer : IComparer<TChild>
        {
            SetSort(typeof(TComparer).AssemblyQualifiedName);
            structure.Alter(x =>
            {
                x.Type = Collection.Set;
            });
            return (T)this;
        }

        public T AsBag()
        {
            structure.Alter(x => x.Type = Collection.Bag);
            return (T)this;
        }

        public T AsList()
        {
            structure.Alter(x => x.Type = Collection.List);
            CreateIndexMapping(null, null, null);
            return (T)this;
        }

        public T AsList(Action<IndexPart> customIndexMapping)
        {
            structure.Alter(x => x.Type = Collection.List);
            CreateIndexMapping(null, null, customIndexMapping);
            return (T)this;
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector)
        {
            return AsMap(indexSelector, _ => {});
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, SortType sort)
        {
            SetSort(sort);
            return AsMap(indexSelector, null, sort);
        }

        public T AsMap(string indexColumnName)
        {
            structure.Alter(x => x.Type = Collection.Map);
            AsIndexedCollection<Int32>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap(string indexColumnName, SortType sort)
        {
            structure.Alter(x => x.Type = Collection.Map);
            SetSort(sort);
            AsIndexedCollection<Int32>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(string indexColumnName)
        {
            structure.Alter(x => x.Type = Collection.Map);
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(string indexColumnName, SortType sort)
        {
            structure.Alter(x => x.Type = Collection.Map);
            SetSort(sort);
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex, TComparer>(string indexColumnName) where TComparer : IComparer<TChild>
        {
            structure.Alter(x => x.Type = Collection.Map);
            SetSort(typeof(TComparer).AssemblyQualifiedName);
            AsIndexedCollection<TIndex>(indexColumnName, null);
            return (T)this;
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            structure.Alter(x => x.Type = Collection.Map);
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        public T AsMap<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping, SortType sort)
        {
            structure.Alter(x => x.Type = Collection.Map);
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        // I'm not proud of this. The fluent interface for maps really needs to be rethought. But I've let maps sit unsupported for way too long
        // so a hack is better than nothing.
        public T AsMap<TIndex>(Action<IndexPart> customIndexMapping, Action<ElementPart> customElementMapping)
        {
            structure.Alter(x => x.Type = Collection.Map);
            AsIndexedCollection<TIndex>(customIndexMapping);
            Element(string.Empty, customElementMapping);
            return (T)this;
        }

        public T AsArray<TIndex>(Expression<Func<TChild, TIndex>> indexSelector)
        {
            return AsArray(indexSelector, null);
        }

        public T AsArray<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            structure.Alter(x => x.Type = Collection.Array);
            return AsIndexedCollection(indexSelector, customIndexMapping);
        }

        public T AsIndexedCollection<TIndex>(string indexColumn, Action<IndexPart> customIndexMapping)
        {
            CreateIndexMapping(typeof(TIndex), null, x =>
            {
                if (!string.IsNullOrEmpty(indexColumn))
                    x.Column(indexColumn);
                
                if (customIndexMapping != null)
                    customIndexMapping(x);
            });
            return (T)this;
        }

        public T AsIndexedCollection<TIndex>(Expression<Func<TChild, TIndex>> indexSelector, Action<IndexPart> customIndexMapping)
        {
            var indexMember = indexSelector.ToMember();
            CreateIndexMapping(typeof(TIndex), indexMember, customIndexMapping);
            return (T)this;
        }

        public T AsIndexedCollection<TIndex>(Action<IndexPart> customIndexMapping)
        {
            CreateIndexMapping(typeof(TIndex), null, customIndexMapping);
            return (T)this;
        }

        private void CreateIndexMapping(Type indexType, Member member, Action<IndexPart> customIndex)
        {
            IMappingStructure<IndexMapping> indexStructure = new FreeStructure<IndexMapping>();
            
            if (member != null)
                indexStructure = new MemberStructure<IndexMapping>(member);

            var part = new IndexPart(indexStructure);

            if (indexType != null)
                part.Type(indexType);

            if (customIndex != null)
                customIndex(part);

            structure.AddChild(indexStructure);
        }

        public T Element(string columnName)
        {
            return Element(columnName, x => {});
        }

        public T Element(string columnName, Action<ElementPart> customElementMapping)
        {
            var elementStructure = new FreeStructure<ElementMapping>();
            var part = new ElementPart(elementStructure);
            part.Type<TChild>();

            if (!string.IsNullOrEmpty(columnName))
                part.Column(columnName);

            if (customElementMapping != null)
                customElementMapping(part);

            structure.AddChild(elementStructure);

            return (T)this;
        }

        /// <summary>
        /// Maps this collection as a collection of components.
        /// </summary>
        /// <param name="action">Component mapping</param>
        public T Component(Action<CompositeElementPart<TChild>> action)
        {
            var compositeElementStructure = new TypeStructure<CompositeElementMapping>(typeof(TChild));
            var part = new CompositeElementPart<TChild>(compositeElementStructure);

            action(part);

            structure.AddChild(compositeElementStructure);

            return (T)this;
        }

        /// <summary>
        /// Sets the table name for this one-to-many.
        /// </summary>
        /// <param name="name">Table name</param>
        public T Table(string name)
        {
            structure.SetValue(Attr.Table, name);
            return (T)this;
        }

        public T ForeignKeyCascadeOnDelete()
        {
            keyStructure.SetValue(Attr.OnDelete, "cascade");
            return (T)this;
        }

        public FetchTypeExpression<T> Fetch
        {
            get { return fetch; }
        }

        /// <summary>
        /// Set the access and naming strategy for this one-to-many.
        /// </summary>
        public AccessStrategyBuilder<T> Access
        {
            get { return access; }
        }

        public OptimisticLockBuilder<T> OptimisticLock
        {
            get { return optimisticLock; }
        }

        public T Persister<TPersister>() where TPersister : IEntityPersister
        {
            structure.SetValue(Attr.Persister, new TypeReference(typeof(TPersister)));
            return (T)this;
        }

        public T Check(string checkSql)
        {
            structure.SetValue(Attr.Check, checkSql);
            return (T)this;
        }

        public T Generic()
        {
            structure.SetValue(Attr.Generic, nextBool);
            nextBool = true;
            return (T)this;
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// Note: This only supports simple cases, use the string overload for more complex clauses.
        /// </summary>
        public T Where(Expression<Func<TChild, bool>> where)
        {
            var sql = ExpressionToSql.Convert(where);

            return Where(sql);
        }

        /// <summary>
        /// Sets the where clause for this one-to-many relationship.
        /// </summary>
        public T Where(string where)
        {
            structure.SetValue(Attr.Where, where);
            return (T)this;
        }

        public T BatchSize(int size)
        {
            structure.SetValue(Attr.BatchSize, size);
            return (T)this;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public T Not
        {
            get
            {
                nextBool = !nextBool;
                return (T)this;
            }
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType<TCollection>()
        {
            return CollectionType(typeof(TCollection));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(Type type)
        {
            return CollectionType(new TypeReference(type));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(string type)
        {
            return CollectionType(new TypeReference(type));
        }

        /// <summary>
        /// Sets a custom collection type
        /// </summary>
        public T CollectionType(TypeReference type)
        {
            structure.SetValue(Attr.CollectionType, type);
            return (T)this;
        }

        public T Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
            return (T)this;
        }

        /// <overloads>
        /// Applies a named filter to this one-to-many.
        /// </overloads>
        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <param name="condition">The condition to apply</param>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public T ApplyFilter<TFilter>(string condition) where TFilter : FilterDefinition, new()
        {
            var filter = new FreeStructure<FilterMapping>();
            
            new FilterPart(filter)
                .Name(new TFilter().Name)
                .Condition(condition);

            structure.AddChild(filter);

            return (T)this;
        }

        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public T ApplyFilter<TFilter>() where TFilter : FilterDefinition, new()
        {
            return ApplyFilter<TFilter>(null);
        }
    }
}
