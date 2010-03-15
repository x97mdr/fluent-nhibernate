using System;
using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Automapping
{
    public class AutoCompositeIdentityPart<T> : CompositeIdentityPart<T>
    {
        private readonly IList<string> mappedProperties;

        public AutoCompositeIdentityPart(IMappingStructure<CompositeIdMapping> structure, IList<string> mappedProperties)
            : base(structure)
        {
            this.mappedProperties = mappedProperties;
        }

        protected override CompositeIdentityPart<T> KeyProperty(Member property, string columnName, Action<KeyPropertyPart> customMapping)
        {
            mappedProperties.Add(property.Name);

            return base.KeyProperty(property, columnName, customMapping);
        }

        protected override CompositeIdentityPart<T> KeyReference(Member property, string columnName, Action<KeyManyToOnePart> customMapping)
        {
            mappedProperties.Add(property.Name);

            return base.KeyReference(property, columnName, customMapping);
        }
    }
}
