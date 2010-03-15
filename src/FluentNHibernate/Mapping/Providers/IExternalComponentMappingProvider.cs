using System;

namespace FluentNHibernate.Mapping.Providers
{
    public interface IExternalComponentMappingProvider : IMappingProvider
    {
        Type Type { get; }
    }
}