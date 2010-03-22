using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Conventions.Inspections
{
    public class KeyPropertyInspector : IKeyPropertyInspector
    {
        private readonly KeyPropertyMapping mapping;

        public KeyPropertyInspector(KeyPropertyMapping mapping)
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

        public Access Access
        {
            get { return Access.FromString(mapping.Access); }
        }

        public string Name
        {
            get { return mapping.Name; }
        }

        public TypeReference Type
        {
            get { return mapping.Type; }
        }

        public IEnumerable<IColumnInspector> Columns
        {
            get
            {
                return mapping.Columns
                    .Select(x => new ColumnInspector(mapping.ContainingEntityType, x))
                    .Cast<IColumnInspector>();
            }
        }
    }
}