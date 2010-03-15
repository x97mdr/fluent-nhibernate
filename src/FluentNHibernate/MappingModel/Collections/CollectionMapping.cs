using System;
using System.Collections.Generic;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Collections
{
    public class Collection
    {
        public static readonly Collection Set = new Collection("set", true);
        public static readonly Collection Bag = new Collection("bag", false);
        public static readonly Collection List = new Collection("list", true);
        public static readonly Collection Array = new Collection("array", true);
        public static readonly Collection Map = new Collection("map", true);

        readonly string elementName;
        readonly bool supportsIndex;

        Collection(string elementName, bool supportsIndex)
        {
            this.elementName = elementName;
            this.supportsIndex = supportsIndex;
        }

        public bool SupportsIndex
        {
            get { return supportsIndex; }
        }

        public string GetElementName()
        {
            return elementName;
        }
    }

    public class CollectionMapping : MappingBase, ITypeAndMemberMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IList<FilterMapping> filters = new List<FilterMapping>();
        
        public Type ContainingEntityType { get; set; }
        public Member Member { get; set; }
        public Collection Type { get; set; }

        public void Initialise(Type type, Member member)
        {
            Name = GetMemberName(member);
            Type = GetCollectionType(member.PropertyType);
            ChildType = GetChildType(member.PropertyType);
            Member = member;

            if (IsCustomCollection(member.PropertyType))
                CollectionType = new TypeReference(member.PropertyType);
        }

        Type GetChildType(Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericArguments()[0];
            if (type.IsArray)
                return type.GetElementType();

            return null;
        }

        string GetMemberName(Member member)
        {
            if (member is MethodMember && member.Name.StartsWith("Get"))
            {
                var name = member.Name.Substring(3);
                return char.ToLower(name[0]) + name.Substring(1);
            }

            return member.Name;
        }

        bool IsCustomCollection(Type type)
        {
            return type.ClosesInterface(typeof(IEnumerable<>)) &&
                !type.Namespace.StartsWith("System") &&
                !type.Namespace.StartsWith("Iesi");
        }

        Collection GetCollectionType(Type type)
        {
            if (type.Namespace == "Iesi.Collections.Generic" || type.Closes(typeof(HashSet<>)))
                return Collection.Set;
            
            return Collection.Bag;
        }

        public IList<FilterMapping> Filters
        {
            get { return filters; }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessCollection(this);

            if (Key != null)
                visitor.Visit(Key);

            if (Element != null)
                visitor.Visit(Element);

            if (CompositeElement != null)
                visitor.Visit(CompositeElement);

            if (Relationship != null)
                visitor.Visit(Relationship);

            foreach (var filter in Filters)
                visitor.Visit(filter);

            if (Cache != null)
                visitor.Visit(Cache);

            if (Type.SupportsIndex && Index != null)
                visitor.Visit(Index);
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public Type ChildType
        {
            get { return values.Get<Type>(Attr.ChildType); }
            set { values.Set(Attr.ChildType, value); }
        }

        public CollectionMapping OtherSide { get; set; }
        public KeyMapping Key { get; set; }
        public ElementMapping Element { get; set; }
        public CompositeElementMapping CompositeElement { get; set; }
        public CacheMapping Cache { get; set; }
        public ICollectionRelationshipMapping Relationship { get; set; }

        public bool Generic
        {
            get { return values.Get<bool>(Attr.Generic); }
            set { values.Set(Attr.Generic, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public bool Inverse
        {
            get { return values.Get<bool>(Attr.Inverse); }
            set { values.Set(Attr.Inverse, value); }
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Access
        {
            get { return values.Get(Attr.Access); }
            set { values.Set(Attr.Access, value); }
        }

        public string TableName
        {
            get { return values.Get(Attr.Table); }
            set { values.Set(Attr.Table, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
        }

        public string Fetch
        {
            get { return values.Get(Attr.Fetch); }
            set { values.Set(Attr.Fetch, value); }
        }

        public string Cascade
        {
            get { return values.Get(Attr.Cascade); }
            set { values.Set(Attr.Cascade, value); }
        }

        public string Where
        {
            get { return values.Get(Attr.Where); }
            set { values.Set(Attr.Where, value); }
        }

        public bool Mutable
        {
            get { return values.Get<bool>(Attr.Mutable); }
            set { values.Set(Attr.Mutable, value); }
        }

        public string Subselect
        {
            get { return values.Get(Attr.Subselect); }
            set { values.Set(Attr.Subselect, value); }
        }

    	public TypeReference Persister
        {
            get { return values.Get<TypeReference>(Attr.Persister); }
            set { values.Set(Attr.Persister, value); }
        }

        public int BatchSize
        {
            get { return values.Get<int>(Attr.BatchSize); }
            set { values.Set(Attr.BatchSize, value); }
        }

        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public TypeReference CollectionType
        {
            get { return values.Get<TypeReference>(Attr.CollectionType); }
            set { values.Set(Attr.CollectionType, value); }
        }

        public string OptimisticLock
        {
            get { return values.Get(Attr.OptimisticLock); }
            set { values.Set(Attr.OptimisticLock, value); }
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public string OrderBy
        {
            get { return values.Get(Attr.OrderBy); }
            set { values.Set(Attr.OrderBy, value); }
        }

        public virtual void AddChild(IMapping child)
        {
            if (child is KeyMapping)
                Key = (KeyMapping)child;
            if (child is IIndexMapping)
                Index = (IIndexMapping)child;
            if (child is ElementMapping)
                Element = (ElementMapping)child;
            if (child is CompositeElementMapping)
                CompositeElement = (CompositeElementMapping)child;
            if (child is CacheMapping)
                Cache = (CacheMapping)child;
            if (child is ICollectionRelationshipMapping)
                Relationship = (ICollectionRelationshipMapping)child;
            if (child is FilterMapping)
                filters.Add((FilterMapping)child);
        }
        
        public virtual void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }

        public string Sort
        {
            get { return values.Get(Attr.Sort); }
            set { values.Set(Attr.Sort, value); }
        }

        public IIndexMapping Index { get; set; }

        public bool Equals(CollectionMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as CollectionMapping);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                {
                    return (base.GetHashCode() * 397) ^ (values != null ? values.GetHashCode() : 0);
                }
            }
        }
    }
}