using System;

namespace FluentNHibernate.Mapping.Providers
{
    public interface IIndeterminateSubclassMappingProvider : IMappingProvider
    {
        Type EntityType { get; }
    }
}