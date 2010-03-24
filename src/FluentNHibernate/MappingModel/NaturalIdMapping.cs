using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class NaturalIdMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();
        readonly IList<PropertyMapping> properties = new List<PropertyMapping>();
        readonly IList<ManyToOneMapping> manyToOnes = new List<ManyToOneMapping>();

        public NaturalIdMapping()
        {
            Mutable = false;
        }

        public bool Mutable
        {
            get { return values.Get<bool>(Attr.Mutable); }
            set { values.Set(Attr.Mutable, value); }
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return properties; }
        }

        public IEnumerable<ManyToOneMapping> ManyToOnes
        {
            get { return manyToOnes; }
        }

        public void AddProperty(PropertyMapping mapping)
        {
            properties.Add(mapping);
        }

        public void AddReference(ManyToOneMapping mapping)
        {
            manyToOnes.Add(mapping);
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessNaturalId(this);

            foreach (var key in properties)
                visitor.Visit(key);

            foreach (var key in manyToOnes)
                visitor.Visit(key);
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public void AddChild(IMapping child)
        {
            if (child is PropertyMapping)
                AddProperty((PropertyMapping)child);
            if (child is ManyToOneMapping)
                AddReference((ManyToOneMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}
