using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel
{
    public class ValueStore : IEnumerable<KeyValuePair<Attr, object>>
    {
        // TODO: Remove this soon
        readonly Dictionary<Attr, object> defaults = new Dictionary<Attr, object>();
        readonly Dictionary<Attr, object> values = new Dictionary<Attr, object>();

        public ValueStore()
        {}

        public ValueStore(IEnumerable<KeyValuePair<Attr, object>> existing)
        {
            existing.Each(x => Set(x.Key, x.Value));
        }

        public void Set(Attr attr, object value)
        {
            values[attr] = value;
        }

        public void SetDefault(Attr attr, object value)
        {
            defaults[attr] = value;
        }

        public string Get(Attr attr)
        {
            return Get<string>(attr);
        }

        public T Get<T>(Attr attr)
        {
            if (values.ContainsKey(attr))
                return (T)values[attr];
            if (defaults.ContainsKey(attr))
                return (T)defaults[attr];

            return default(T);
        }

        public void Merge(ValueStore otherValues)
        {
            foreach (var pair in otherValues.defaults)
                SetDefault(pair.Key, pair.Value);

            foreach (var pair in otherValues)
                Set(pair.Key, pair.Value);
        }

        public bool HasUserDefinedValue(Attr attr)
        {
            return values.ContainsKey(attr);
        }

        public bool HasValue(Attr attr)
        {
            return HasUserDefinedValue(attr) || defaults.ContainsKey(attr);
        }

        public IEnumerator<KeyValuePair<Attr, object>> GetEnumerator()
        {
            return values.Concat(defaults).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(ValueStore other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.defaults.ContentEquals(defaults) && other.values.ContentEquals(values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ValueStore)) return false;
            return Equals((ValueStore)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((defaults != null ? defaults.GetHashCode() : 0) * 397) ^ (values != null ? values.GetHashCode() : 0);
            }
        }
    }
}