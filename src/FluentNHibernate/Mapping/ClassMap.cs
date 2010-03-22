using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;
using NHibernate.Persister.Entity;

namespace FluentNHibernate.Mapping
{
    public class ClassMap<T> : ClasslikeMapBase<T>, IMappingProvider
    {
        bool nextBool = true;

        protected TuplizerMapping tuplizerMapping;

        readonly IMappingStructure<ClassMapping> structure;
        readonly IMappingStructure<HibernateMapping> mappingStructure;
        IMappingStructure<CacheMapping> cacheStructure;

        public ClassMap()
            : this(new TypeStructure<ClassMapping>(typeof(T)), new FreeStructure<HibernateMapping>())
        {}

        ClassMap(IMappingStructure<ClassMapping> structure, IMappingStructure<HibernateMapping> mappingStructure)
            : base(structure)
        {
            this.structure = structure;
            this.mappingStructure = mappingStructure;
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

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            return new FluentMapUserDefinedMappings(typeof(T), structure);
        }

        public HibernateMapping GetHibernateMapping()
        {
            return ((IHibernateMappingProvider)HibernateMapping).GetHibernateMapping();
        }

        public HibernateMappingPart HibernateMapping
        {
            get { return new HibernateMappingPart(mappingStructure); }
        }

        public virtual NaturalIdPart<T> NaturalId()
        {
            var natrualIdStructure = new FreeStructure<NaturalIdMapping>();
            var part = new NaturalIdPart<T>(natrualIdStructure);

            structure.AddChild(natrualIdStructure);

            return part;
        }

        public virtual CompositeIdentityPart<T> CompositeId()
        {
            var compositeIdStructure = new FreeStructure<CompositeIdMapping>();
            var part = new CompositeIdentityPart<T>(compositeIdStructure);

            structure.AddChild(compositeIdStructure);

            return part;
        }

        public virtual CompositeIdentityPart<TId> CompositeId<TId>(Expression<Func<T, TId>> expression)
        {
            var compositeIdStructure = new MemberStructure<CompositeIdMapping>(expression.ToMember());
            var part = new CompositeIdentityPart<TId>(compositeIdStructure);

            structure.AddChild(compositeIdStructure);

            return part;
        }

        public VersionPart Version(Expression<Func<T, object>> expression)
        {
            return Version(expression.ToMember());
        }

        protected virtual VersionPart Version(Member property)
        {
            var versionStructure = new MemberStructure<VersionMapping>(property);
            var versionPart = new VersionPart(versionStructure);

            structure.AddChild(versionStructure);

            return versionPart;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName, TDiscriminator baseClassDiscriminator)
        {
            var discriminatorStructure = new TypeStructure<DiscriminatorMapping>(typeof(TDiscriminator));
            var part = new DiscriminatorPart(discriminatorStructure, structure);

            part.Column(columnName);

            structure.SetValue(Attr.DiscriminatorValue, baseClassDiscriminator);
            structure.AddChild(discriminatorStructure);

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn<TDiscriminator>(string columnName)
        {
            var discriminatorStructure = new TypeStructure<DiscriminatorMapping>(typeof(TDiscriminator));
            var part = new DiscriminatorPart(discriminatorStructure, structure);

            part.Column(columnName);

            structure.AddChild(discriminatorStructure);

            return part;
        }

        public virtual DiscriminatorPart DiscriminateSubClassesOnColumn(string columnName)
        {
            return DiscriminateSubClassesOnColumn<string>(columnName);
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            return Id(expression, null);
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(Expression<Func<T, TReturn>> expression, string column)
        {
            var idStructure = new MemberStructure<IdMapping>(expression.ToMember());
            var part = new IdentityPart<TReturn>(idStructure);

            if (column != null)
                part.Column(column);

            structure.AddChild(idStructure);
            
            return part;
        }

        public virtual IdentityPart<TReturn> Id<TReturn>(string column)
        {
            var idStructure = new FreeStructure<IdMapping>();
            var part = new IdentityPart<TReturn>(idStructure);
            
            if (column != null)
                part.Column(column);

            structure.AddChild(idStructure);

            return part;
        } 

        [Obsolete("Inline definitions of subclasses are depreciated. Please create a derived class from SubclassMap in the same way you do with ClassMap.")]
        public virtual void JoinedSubClass<TSubclass>(string keyColumn, Action<JoinedSubClassPart<TSubclass>> action) where TSubclass : T
        {
            var subclassStructure = new SubclassStructure(SubclassType.JoinedSubclass, typeof(TSubclass));
            var subclass = new JoinedSubClassPart<TSubclass>(subclassStructure);

            subclass.KeyColumns.Add(keyColumn);

            action(subclass);

            structure.AddChild(subclassStructure);
        }

        /// <summary>
        /// Sets the hibernate-mapping schema for this class.
        /// </summary>
        /// <param name="schema">Schema name</param>
        public void Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
        }

        /// <summary>
        /// Sets the table for the class.
        /// </summary>
        /// <param name="tableName">Table name</param>
        public void Table(string tableName)
        {
            structure.SetValue(Attr.Table, tableName);
        }

        /// <summary>
        /// Inverse next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ClassMap<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        /// <summary>
        /// Sets this entity to be lazy-loaded (overrides the default lazy load configuration).
        /// </summary>
        public void LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets additional tables for the class via the NH 2.0 Join element.
        /// </summary>
        /// <param name="tableName">Joined table name</param>
        /// <param name="action">Joined table mapping</param>
        public void Join(string tableName, Action<JoinPart<T>> action)
        {
            var joinStructure = new TypeStructure<JoinMapping>(typeof(T));
            var join = new JoinPart<T>(joinStructure);

            join.Table(tableName);

            action(join);

            structure.AddChild(joinStructure);
        }

        /// <summary>
        /// Imports an existing type for use in the mapping.
        /// </summary>
        /// <typeparam name="TImport">Type to import.</typeparam>
        public ImportPart ImportType<TImport>()
        {
            var import = new TypeStructure<ImportMapping>(typeof(TImport));
            var part = new ImportPart(import);
            
            mappingStructure.AddChild(import);

            return part;
        }

        /// <summary>
        /// Set the mutability of this class, sets the mutable attribute.
        /// </summary>
        public void ReadOnly()
        {
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic update
        /// </summary>
        public void DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
        }

        /// <summary>
        /// Sets this entity to be dynamic insert
        /// </summary>
        public void DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
        }

        public ClassMap<T> BatchSize(int size)
        {
            structure.SetValue(Attr.BatchSize, size);
            return this;
        }

        /// <summary>
        /// Sets the optimistic locking strategy
        /// </summary>
        public OptimisticLockBuilder<ClassMap<T>> OptimisticLock
        {
            get { return new OptimisticLockBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.OptimisticLock, value)); }
        }

        public PolymorphismBuilder<ClassMap<T>> Polymorphism
        {
            get { return new PolymorphismBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.Polymorphism, value)); }
        }

        public SchemaActionBuilder<ClassMap<T>> SchemaAction
        {
            get { return new SchemaActionBuilder<ClassMap<T>>(this, value => structure.SetValue(Attr.SchemaAction, value)); }
        }

        public void CheckConstraint(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
        }

        public void Persister<TPersister>() where TPersister : IEntityPersister
        {
            Persister(typeof(TPersister));
        }

        private void Persister(Type type)
        {
            Persister(type.AssemblyQualifiedName);
        }

        private void Persister(string type)
        {
            structure.SetValue(Attr.Persister, type);
        }

        public void Proxy<TProxy>()
        {
            Proxy(typeof(TProxy));
        }

        public void Proxy(Type type)
        {
            Proxy(type.AssemblyQualifiedName);
        }

        public void Proxy(string type)
        {
            structure.SetValue(Attr.Proxy, type);
        }

        public void SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
        }

		/// <summary>
		/// Defines a SQL 'where' clause used when retrieving objects of this type.
		/// </summary>
    	public void Where(string where)
    	{
            structure.SetValue(Attr.Where, where);
    	}

        /// <summary>
        /// Sets the SQL statement used in subselect fetching.
        /// </summary>
        /// <param name="subselectSql">Subselect SQL Query</param>
        public void Subselect(string subselectSql)
        {
            structure.SetValue(Attr.Subselect, subselectSql);
        }

        /// <summary>
        /// Specifies an entity-name.
        /// </summary>
        /// <remarks>See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx</remarks>
        public void EntityName(string entityName)
        {
            structure.SetValue(Attr.EntityName, entityName);
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
        public ClassMap<T> ApplyFilter<TFilter>(string condition) where TFilter : FilterDefinition, new()
        {
            var filter = new FreeStructure<FilterMapping>();

            new FilterPart(filter)
                .Name(new TFilter().Name)
                .Condition(condition);

            structure.AddChild(filter);

            return this;
        }

        /// <summary>
        /// Applies a named filter to this one-to-many.
        /// </summary>
        /// <typeparam name="TFilter">
        /// The type of a <see cref="FilterDefinition"/> implementation
        /// defining the filter to apply.
        /// </typeparam>
        public ClassMap<T> ApplyFilter<TFilter>() where TFilter : FilterDefinition, new()
        {
            return ApplyFilter<TFilter>(null);
        }

        public ClassMap<T> Tuplizer(TuplizerMode mode, Type tuplizerType)
        {
            var tuplizer = new FreeStructure<TuplizerMapping>();
            tuplizer.SetValue(Attr.Mode, mode);
            tuplizer.SetValue(Attr.Type, new TypeReference(tuplizerType));

            structure.AddChild(tuplizer);

            return this;
        }
    }
}