using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ImportMapping : MappingBase, IMapping, ITypeMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessImport(this);
        }

        public string Rename
        {
            get { return values.Get(Attr.Rename); }
            set { values.Set(Attr.Rename, value); }
        }

        public TypeReference Class
        {
            get { return values.Get<TypeReference>(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(ImportMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ImportMapping)) return false;
            return Equals((ImportMapping)obj);
        }

        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
        }

        public void AddChild(IMapping child)
        {
            
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }

        public void Initialise(Type type)
        {
            Class = new TypeReference(type);
        }
    }
}