using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Identity
{
    public class GeneratorMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly List<ParamMapping> parameters = new List<ParamMapping>();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessGenerator(this);
        }

        public string Class
        {
            get { return values.Get(Attr.Class); }
            set { values.Set(Attr.Class, value); }
        }

        public void AddParam(ParamMapping param)
        {
            parameters.Add(param);
        }

        public IEnumerable<ParamMapping> Params
        {
            get { return parameters; }
        }

        public Type ContainingEntityType { get; set; }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(GeneratorMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values) &&
                other.Params.ContentEquals(Params) &&
                Equals(other.ContainingEntityType, ContainingEntityType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(GeneratorMapping)) return false;
            return Equals((GeneratorMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (values != null ? values.GetHashCode() : 0);
                result = (result * 397) ^ (Params != null ? Params.GetHashCode() : 0);
                result = (result * 397) ^ (ContainingEntityType != null ? ContainingEntityType.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            if (child is ParamMapping)
                AddParam((ParamMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}