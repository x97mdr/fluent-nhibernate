using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions.Inspections
{
    public interface IGeneratorInspector : IInspector
    {
        string Class { get; }
        IDictionary<string, string> Params { get; }
    }

    public class GeneratorInspector : IGeneratorInspector
    {
        private readonly InspectorModelMapper<IGeneratorInspector, GeneratorMapping> propertyMappings = new InspectorModelMapper<IGeneratorInspector, GeneratorMapping>();
        private readonly GeneratorMapping mapping;

        public GeneratorInspector(GeneratorMapping mapping)
        {
            this.mapping = mapping;
        }

        public Type EntityType
        {
            get { return mapping.ContainingEntityType; }
        }

        public string StringIdentifierForModel
        {
            get { return mapping.Class; }
        }

        public bool IsSet(Member property)
        {
            return mapping.IsSpecified(propertyMappings.Get(property));
        }

        public string Class
        {
            get { return mapping.Class; }
        }

        public IDictionary<string, string> Params
        {
            get
            {
                var parameters = new Dictionary<string, string>();

                mapping.Params
                    .Each(x => parameters.Add(x.Name, x.Value));

                return parameters;
            }
        }
    }
}