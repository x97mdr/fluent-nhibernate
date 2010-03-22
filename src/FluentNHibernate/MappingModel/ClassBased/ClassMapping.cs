using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class ClassMapping : ClassMappingBase, ITypeMapping
    {
        readonly ValueStore values = new ValueStore();

        public ClassMapping()
        {}

        public ClassMapping(Type type)
        {
            Initialise(type);
        }

        public void Initialise(Type type)
        {
            Type = type;

            Name = type.AssemblyQualifiedName;
            values.SetDefault(Attr.Table, GetDefaultTableName(type));
        }

        private static string GetDefaultTableName(Type type)
        {
            var tableName = type.Name;

            if (type.IsGenericType)
            {
                // special case for generics: GenericType_GenericParameterType
                tableName = type.Name.Substring(0, type.Name.IndexOf('`'));

                foreach (var argument in type.GetGenericArguments())
                {
                    tableName += "_";
                    tableName += argument.Name;
                }
            }

            return "`" + tableName + "`";
        }

        public IIdentityMapping Id { get; set; }
        public NaturalIdMapping NaturalId { get; set; }
        public CacheMapping Cache { get; set; }
        public VersionMapping Version { get; set; }
        public DiscriminatorMapping Discriminator { get; set; }
        public TuplizerMapping Tuplizer { get; set; }

        public override string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public override Type Type
        {
            get { return values.Get<Type>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessClass(this);            

            if (Id != null)
                visitor.Visit(Id);

            if (NaturalId != null)
                visitor.Visit(NaturalId);

            if (Discriminator != null)
                visitor.Visit(Discriminator);

            if (Cache != null)
                visitor.Visit(Cache);

            if (Version != null)
                visitor.Visit(Version);

            if (Tuplizer != null)
                visitor.Visit(Tuplizer);

            base.AcceptVisitor(visitor);
        }

        public string TableName
        {
            get { return values.Get(Attr.Table); }
            set { values.Set(Attr.Table, value); }
        }

        public int BatchSize
        {
            get { return values.Get<int>(Attr.BatchSize); }
            set { values.Set(Attr.BatchSize, value); }
        }

        public object DiscriminatorValue
        {
            get { return values.Get(Attr.DiscriminatorValue); }
            set { values.Set(Attr.DiscriminatorValue, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public bool Mutable
        {
            get { return values.Get<bool>(Attr.Mutable); }
            set { values.Set(Attr.Mutable, value); }
        }

        public bool DynamicUpdate
        {
            get { return values.Get<bool>(Attr.DynamicUpdate); }
            set { values.Set(Attr.DynamicUpdate, value); }
        }

        public bool DynamicInsert
        {
            get { return values.Get<bool>(Attr.DynamicInsert); }
            set { values.Set(Attr.DynamicInsert, value); }
        }

        public string OptimisticLock
        {
            get { return values.Get(Attr.OptimisticLock); }
            set { values.Set(Attr.OptimisticLock, value); }
        }

        public string Polymorphism
        {
            get { return values.Get(Attr.Polymorphism); }
            set { values.Set(Attr.Polymorphism, value); }
        }

        public string Persister
        {
            get { return values.Get(Attr.Persister); }
            set { values.Set(Attr.Persister, value); }
        }

        public string Where
        {
            get { return values.Get(Attr.Where); }
            set { values.Set(Attr.Where, value); }
        }

        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public string Proxy
        {
            get { return values.Get(Attr.Proxy); }
            set { values.Set(Attr.Proxy, value); }
        }

        public bool SelectBeforeUpdate
        {
            get { return values.Get<bool>(Attr.SelectBeforeUpdate); }
            set { values.Set(Attr.SelectBeforeUpdate, value); }
        }

        public bool Abstract
        {
            get { return values.Get<bool>(Attr.Abstract); }
            set { values.Set(Attr.Abstract, value); }
        }

        public string Subselect
        {
            get { return values.Get(Attr.Subselect); }
            set { values.Set(Attr.Subselect, value); }
        }

        public string SchemaAction
        {
            get { return values.Get(Attr.SchemaAction); }
            set { values.Set(Attr.SchemaAction, value); }
        }

        public string EntityName
        {
            get { return values.Get(Attr.EntityName); }
            set { values.Set(Attr.EntityName, value); }
        }       

        public override bool HasUserDefinedValue(Attr property)
        {
            if (property == Attr.Id)
                return Id != null;
            if (property == Attr.Cache)
                return Cache != null;
            if (property == Attr.Discriminator)
                return Discriminator != null;
            if (property == Attr.Version)
                return Version != null;

            return values.HasUserDefinedValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(ClassMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ClassMapping)) return false;
            return Equals((ClassMapping)obj);
        }

        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
        }

        public override void AddChild(IMapping child)
        {
            base.AddChild(child);

            if (child is CacheMapping)
                Cache = (CacheMapping)child;
            if (child is IIdentityMapping)
                Id = (IIdentityMapping)child;
            if (child is NaturalIdMapping)
                NaturalId = (NaturalIdMapping)child;
            if (child is VersionMapping)
                Version = (VersionMapping)child;
            if (child is DiscriminatorMapping)
                Discriminator = (DiscriminatorMapping)child;
            if (child is TuplizerMapping)
                Tuplizer = (TuplizerMapping)child;
            if (child is FilterMapping)
                AddFilter((FilterMapping)child);
            if (child is StoredProcedureMapping)
                AddStoredProcedure((StoredProcedureMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}