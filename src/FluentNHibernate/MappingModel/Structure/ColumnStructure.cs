using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class IdentifierColumnStructure : ColumnStructure<IdentifierColumnMapping>
    {
        public IdentifierColumnStructure(IMappingStructure parent)
            : base(parent)
        {}
    }

    public class ColumnStructure : ColumnStructure<ColumnMapping>
    {
        public ColumnStructure(IMappingStructure parent)
            : base(parent)
        { }
    }

    public abstract class ColumnStructure<T> : BaseMappingStructure<T>
        where T : IMapping, new()
    {
        readonly IMappingStructure parent;

        public ColumnStructure(IMappingStructure parent)
        {
            this.parent = parent;
        }

        public override T CreateMappingNode()
        {
            var mapping = new T();

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }

        public override IEnumerable<KeyValuePair<Attr, object>> Values
        {
            get
            {
                return Merge(base.Values, parent.Values);
            }
        }

        /// <summary>
        /// Merges two key collections, overwriting any values in the left with ones from the
        /// right.
        /// </summary>
        private static IEnumerable<KeyValuePair<Attr, object>> Merge(IEnumerable<KeyValuePair<Attr, object>> left, IEnumerable<KeyValuePair<Attr, object>> right)
        {
            var merged = new Dictionary<Attr, object>();

            left.Each(x => merged[x.Key] = x.Value);
            right.Each(x => merged[x.Key] = x.Value);

            return merged;
        }
    }
}