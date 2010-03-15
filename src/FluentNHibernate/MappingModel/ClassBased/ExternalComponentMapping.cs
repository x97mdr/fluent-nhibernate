using System;

namespace FluentNHibernate.MappingModel.ClassBased
{
    /// <summary>
    /// A component that is declared external to a class mapping.
    /// </summary>
    public class ExternalComponentMapping : ComponentMapping
    {
        public ExternalComponentMapping()
        {}

        public ExternalComponentMapping(Type type)
            : base(null)
        {}
    }
}