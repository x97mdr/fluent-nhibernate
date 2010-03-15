using System;
using System.Collections.Generic;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class ParamMapping : MappingBase, IMapping
    {
        readonly ValueStore values = new ValueStore();

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool IsSpecified(string property)
        {
            throw new NotImplementedException();
        }

        public void AddChild(IMapping child)
        {
        }

        public void UpdateValues(IEnumerable<KeyValuePair<Attr, object>> otherValues)
        {
            values.Merge(otherValues);
        }

        public string Name
        {
            get { return values.Get(Attr.Name); }
            set { values.Set(Attr.Name, value); }
        }

        public string Value
        {
            get { return values.Get(Attr.Value); }
            set { values.Set(Attr.Value, value); }
        }
    }
}