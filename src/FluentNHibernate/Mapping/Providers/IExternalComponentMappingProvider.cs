using System;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping.Providers
{
    public interface IExternalComponentMappingProvider : IMappingProvider
    {
        Type Type { get; }
    }
}