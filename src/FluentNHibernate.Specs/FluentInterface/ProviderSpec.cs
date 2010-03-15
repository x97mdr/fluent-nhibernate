using System;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Specs.FluentInterface
{
    public abstract class ProviderSpec
    {
        public static ClassMapping map_as_class<T>(Action<ClassMap<T>> setup)
        {
            var provider = new ClassMap<T>();

            setup(provider);

            return (ClassMapping)((IMappingProvider)provider).GetUserDefinedMappings().Structure;
        }

        public static SubclassMapping map_as_subclass<T>(Action<SubclassMap<T>> setup)
        {
            var provider = new SubclassMap<T>();

            setup(provider);

            var userMappings = ((IIndeterminateSubclassMappingProvider)provider).GetUserDefinedMappings();
            var mapping = (SubclassMapping)userMappings.Structure;
            mapping.SubclassType = SubclassType.Subclass;

            return mapping;
        }
    }
}