using System;
using System.Reflection;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Inspections
{
    public class ParentInspector : IParentInspector
    {
        private readonly ParentMapping mapping;

        public ParentInspector(ParentMapping mapping)
        {
            this.mapping = mapping;
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Name; }
        }

        public bool IsSet(Attr property)
        {
            return mapping.HasUserDefinedValue(property);
        }

        public string Name
        {
            get { return mapping.Name; }
        }
    }
}