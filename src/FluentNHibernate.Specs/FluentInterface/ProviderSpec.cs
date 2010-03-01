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

            var mapping = new ClassMapping();
            var result = ((IMappingProvider)provider).GetClassMapping();
            result.ApplyTo(mapping);

            return mapping;
        }

        public static SubclassMapping map_as_subclass<T>(Action<SubclassMap<T>> setup)
        {
            var provider = new SubclassMap<T>();
            var mapping = new SubclassMapping(SubclassType.Subclass);

            setup(provider);

            var result = ((IMappingProvider)provider).GetClassMapping();
            result.ApplyTo(mapping);

            return mapping;
        }
    }
}