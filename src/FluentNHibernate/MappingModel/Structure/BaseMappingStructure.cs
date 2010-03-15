using System;
using System.Collections.Generic;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public abstract class BaseMappingStructure<T> : IMappingStructure<T>
        where T : IMapping
    {
        readonly Dictionary<Attr, object> values = new Dictionary<Attr, object>();
        readonly List<IMappingStructure> children = new List<IMappingStructure>();
        readonly List<Action<T>> alterations = new List<Action<T>>();
        T node;

        IMapping IMappingStructure.CreateMappingNode()
        {
            return node = CreateMappingNode();
        }

        public void ApplyCustomisations()
        {
            node.UpdateValues(Values);
            alterations.Each(x => x(node));

            Children.Each(x => x.ApplyCustomisations());
        }

        public abstract T CreateMappingNode();
        
        public void SetValue(Attr key, object value)
        {
            values[key] = value;
        }

        public void RemoveChildrenMatching(Predicate<IMappingStructure> predicate)
        {
            children.RemoveAll(predicate);
        }

        public void Alter(Action<T> alteration)
        {
            alterations.Add(alteration);
        }

        public virtual IEnumerable<KeyValuePair<Attr, object>> Values
        {
            get { return values; }
        }

        public IEnumerable<IMappingStructure> Children
        {
            get { return children.AsReadOnly(); }
        }

        public void AddChild(IMappingStructure child)
        {
            children.Add(child);
        }
    }
}