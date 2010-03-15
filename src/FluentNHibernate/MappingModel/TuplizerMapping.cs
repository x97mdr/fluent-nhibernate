using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class TuplizerMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessTuplizer(this);
        }

        public TuplizerMode Mode
        {
            get { return values.Get<TuplizerMode>(Attr.Mode); }
            set { values.Set(Attr.Mode, value); }
        }

        public TypeReference Type
        {
            get { return values.Get<TypeReference>(Attr.Type); }
            set { values.Set(Attr.Type, value); }
        }

        public override bool IsSpecified(string property)
        {
            return false;
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(TuplizerMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TuplizerMapping)) return false;
            return Equals((TuplizerMapping)obj);
        }

        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
        }

        public void AddChild(IMapping child)
        {
            
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }
    }

    public enum TuplizerMode
    {
        Poco,
        Xml,
        DynamicMap
    }
}