using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;
using NHibernate.Type;

namespace FluentNHibernate.MappingModel
{
    public class FilterDefinitionMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IDictionary<string, IType> parameters = new Dictionary<string, IType>();

        public IDictionary<string, IType> Parameters
        {
            get { return parameters; }
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Condition
        {
            get { return values.Get(Attr.Condition); }
            set { values.Set(Attr.Condition, value); }
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessFilterDefinition(this);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return values.HasValue(property);
        }

        public bool Equals(FilterDefinitionMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.parameters.ContentEquals(parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FilterDefinitionMapping)) return false;
            return Equals((FilterDefinitionMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((values != null ? values.GetHashCode() : 0) * 397) ^ (parameters != null ? parameters.GetHashCode() : 0);
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
