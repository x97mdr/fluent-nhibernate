using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public class SubclassMapping : ClassMappingBase, ITypeMapping
    {
        public SubclassType SubclassType { get; set; }
        readonly ValueStore values = new ValueStore();

        public SubclassMapping(SubclassType subclassType)
        {
            SubclassType = subclassType;

            Key = new KeyMapping();
        }

        public void Initialise(Type type)
        {
            Name = type.AssemblyQualifiedName;
            Type = type;
            values.SetDefault(Attr.Table, "`" + type.Name + "`");
            values.SetDefault(Attr.DiscriminatorValue, type.Name);

            Key.AddDefaultColumn(new ColumnMapping { Name = type.BaseType.Name + "_id" });
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessSubclass(this);

            if (SubclassType == SubclassType.JoinedSubclass && Key != null)
                visitor.Visit(Key);

            base.AcceptVisitor(visitor);
        }

        public KeyMapping Key { get; set; }

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

        public object DiscriminatorValue
        {
            get { return values.Get<object>(Attr.DiscriminatorValue); }
            set { values.Set(Attr.DiscriminatorValue, value); }
        }

        public string Extends
        {
            get { return values.Get(Attr.Extends); }
            set { values.Set(Attr.Extends, value); }
        }

        public bool Lazy
        {
            get { return values.Get<bool>(Attr.Lazy); }
            set { values.Set(Attr.Lazy, value); }
        }

        public string Proxy
        {
            get { return values.Get(Attr.Proxy); }
            set { values.Set(Attr.Proxy, value); }
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

        public string EntityName
        {
            get { return values.Get(Attr.EntityName); }
            set { values.Set(Attr.EntityName, value); }
        }

        public string TableName
        {
            get { return values.Get(Attr.Table); }
            set { values.Set(Attr.Table, value); }
        }


        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
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

        public override bool HasUserDefinedValue(Attr property)
        {
            if (property == Attr.Key)
                return Key != null;

            return values.HasUserDefinedValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(SubclassMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubclassMapping);
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

        public override void AddChild(IMapping child)
        {
            base.AddChild(child);

            if (child is KeyMapping)
                Key = (KeyMapping)child;
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }

        public override string ToString()
        {
            return string.Format("SubclassMapping({0})", Type.Name);
        }
    }
}