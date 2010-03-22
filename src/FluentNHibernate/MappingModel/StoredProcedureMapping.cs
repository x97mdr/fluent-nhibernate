using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class StoredProcedureMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public StoredProcedureMapping()
        {
            Check = "rowcount";
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public Type Type
        {
            get { return values.Get<Type>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessStoredProcedure(this);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public string Check
        {
            get { return values.Get(Attr.Check); }
            set { values.Set(Attr.Check, value); }
        }

        public string SPType
        {
            get { return values.Get(Attr.SPType); }
            set { values.Set(Attr.SPType, value); }
        }     
        
        public string Query
        {
            get { return values.Get(Attr.Query); }
            set { values.Set(Attr.Query, value); }
        }

        public bool Equals(StoredProcedureMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as StoredProcedureMapping);
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

        public void AddChild(IMapping child)
        {
            
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}
