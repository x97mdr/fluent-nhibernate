using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.Sources;

namespace FluentNHibernate.Specs.Automapping.Fixtures
{
    internal class StubTypeSource : ITypeSource
    {
        private readonly IEnumerable<Type> types;

        public StubTypeSource(Type type)
            : this(new[] { type })
        { }

        public StubTypeSource(IEnumerable<Type> types)
        {
            this.types = types;
        }

        public IEnumerable<Type> GetTypes()
        {
            return types;
        }
    }

    internal class StubMappingSource : IMappingSource
    {
        readonly IMappingProvider[] providers;

        public StubMappingSource(IMappingProvider provider)
            : this(new[] { provider })
        {}

        public StubMappingSource(params IMappingProvider[] providers)
        {
            this.providers = providers;
        }

        public IEnumerable<IMappingResult> GetResults()
        {
            return providers.Select(x => x.GetClassMapping());
        }
    }
}
