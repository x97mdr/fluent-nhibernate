using System;

namespace FluentNHibernate.Mapping.Providers
{
    public interface IReferenceComponentMappingProvider
    {
        Type Type { get; }
    }
}